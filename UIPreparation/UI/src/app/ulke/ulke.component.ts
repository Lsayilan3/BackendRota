import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Ulke } from './models/Ulke';
import { UlkeService } from './services/Ulke.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-ulke',
	templateUrl: './ulke.component.html',
	styleUrls: ['./ulke.component.scss']
})
export class UlkeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['ulkeId','baslik','aciklama','foto','yayin','sira', 'update','delete','file'];

	ulkeList:Ulke[];
	ulke:Ulke=new Ulke();

	ulkeAddForm: FormGroup;
	photoForm: FormGroup;



	ulkeId:number;

	constructor(private ulkeService:UlkeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getUlkeList();
    }

	ngOnInit() {

		this.createUlkeAddForm();
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
		formData.append('ulkeId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.ulkeService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getUlkeList();
				console.log(data);
				this.alertifyService.success(data);
})
	}

	getUlkeList() {
		this.ulkeService.getUlkeList().subscribe(data => {
			this.ulkeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.ulkeAddForm.valid) {
			this.ulke = Object.assign({}, this.ulkeAddForm.value)

			if (this.ulke.ulkeId == 0)
				this.addUlke();
			else
				this.updateUlke();
		}

	}

	addUlke(){

		this.ulkeService.addUlke(this.ulke).subscribe(data => {
			this.getUlkeList();
			this.ulke = new Ulke();
			jQuery('#ulke').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ulkeAddForm);

		})

	}

	updateUlke(){

		this.ulkeService.updateUlke(this.ulke).subscribe(data => {

			var index=this.ulkeList.findIndex(x=>x.ulkeId==this.ulke.ulkeId);
			this.ulkeList[index]=this.ulke;
			this.dataSource = new MatTableDataSource(this.ulkeList);
            this.configDataTable();
			this.ulke = new Ulke();
			jQuery('#ulke').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ulkeAddForm);

		})

	}

	createUlkeAddForm() {
		this.ulkeAddForm = this.formBuilder.group({		
			ulkeId : [0],
baslik : [""],
aciklama : [""],
foto : [""],
yayin : [0],
sira : [0]
		})
	}

	deleteUlke(ulkeId:number){
		this.ulkeService.deleteUlke(ulkeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.ulkeList=this.ulkeList.filter(x=> x.ulkeId!=ulkeId);
			this.dataSource = new MatTableDataSource(this.ulkeList);
			this.configDataTable();
		})
	}

	getUlkeById(ulkeId:number){
		this.clearFormGroup(this.ulkeAddForm);
		this.ulkeService.getUlkeById(ulkeId).subscribe(data=>{
			this.ulke=data;
			this.ulkeAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'ulkeId')
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
