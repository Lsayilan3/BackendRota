import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Destek } from './models/Destek';
import { DestekService } from './services/Destek.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-destek',
	templateUrl: './destek.component.html',
	styleUrls: ['./destek.component.scss']
})
export class DestekComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['destekId','foto', 'update','delete','file'];

	destekList:Destek[];
	destek:Destek=new Destek();

	destekAddForm: FormGroup;
	photoForm: FormGroup;

	destekId:number;

	constructor(private destekService:DestekService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getDestekList();
    }

	ngOnInit() {

		this.createDestekAddForm();
	}
	uploadFile(event) {
		const file = (event.target as HTMLInputElement).files[0];
		this.photoForm.patchValue({
		  file: file,
		});
		this.photoForm.get('file').updateValueAndValidity();
		
	  }

	upFile( id : number){
		this.photoForm = this.formBuilder.group({		
			id : [id],
file : ["", Validators.required]
		})
	}

	addPhotoSave(){
		var formData: any = new FormData();
		formData.append('destekId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.destekService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getDestekList();
				console.log(data);
				this.alertifyService.success(data);
})
	}

	getDestekList() {
		this.destekService.getDestekList().subscribe(data => {
			this.destekList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.destekAddForm.valid) {
			this.destek = Object.assign({}, this.destekAddForm.value)

			if (this.destek.destekId == 0)
				this.addDestek();
			else
				this.updateDestek();
		}

	}

	addDestek(){

		this.destekService.addDestek(this.destek).subscribe(data => {
			this.getDestekList();
			this.destek = new Destek();
			jQuery('#destek').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.destekAddForm);

		})

	}

	updateDestek(){

		this.destekService.updateDestek(this.destek).subscribe(data => {

			var index=this.destekList.findIndex(x=>x.destekId==this.destek.destekId);
			this.destekList[index]=this.destek;
			this.dataSource = new MatTableDataSource(this.destekList);
            this.configDataTable();
			this.destek = new Destek();
			jQuery('#destek').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.destekAddForm);

		})

	}

	createDestekAddForm() {
		this.destekAddForm = this.formBuilder.group({		
			destekId : [0],
foto : ["", Validators.required]
		})
	}

	deleteDestek(destekId:number){
		this.destekService.deleteDestek(destekId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.destekList=this.destekList.filter(x=> x.destekId!=destekId);
			this.dataSource = new MatTableDataSource(this.destekList);
			this.configDataTable();
		})
	}

	getDestekById(destekId:number){
		this.clearFormGroup(this.destekAddForm);
		this.destekService.getDestekById(destekId).subscribe(data=>{
			this.destek=data;
			this.destekAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'destekId')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
