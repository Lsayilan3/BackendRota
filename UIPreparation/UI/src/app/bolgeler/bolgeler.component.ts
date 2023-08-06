import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Bolgeler } from './models/Bolgeler';
import { BolgelerService } from './services/Bolgeler.service';
import { environment } from 'environments/environment';
import { UlkeService } from 'app/ulke/services/Ulke.service';
import { Ulke } from 'app/ulke/models/Ulke';

declare var jQuery: any;

@Component({
	selector: 'app-bolgeler',
	templateUrl: './bolgeler.component.html',
	styleUrls: ['./bolgeler.component.scss']
})
export class BolgelerComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['bolgelerId','ulkeId','foto','baslik','aciklama','yayin','sira', 'update','delete','file'];

	bolgelerList:Bolgeler[];
	bolgeler:Bolgeler=new Bolgeler();

	bolgelerAddForm: FormGroup;
	photoForm: FormGroup;

	ulkeList:Ulke[];
	bolgelerId:number;

	constructor(private ulkeService:UlkeService, private bolgelerService:BolgelerService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBolgelerList();
		this.ulkeService.getUlkeList().subscribe(data=>this.ulkeList=data);
    }

	ngOnInit() {
		this.ulkeService.getUlkeList().subscribe(data=>this.ulkeList=data);
		this.createBolgelerAddForm();
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
		formData.append('bolgelerId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.bolgelerService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getBolgelerList();
				console.log(data);
				this.alertifyService.success(data);
})
	}



	getBolgelerList() {
		this.bolgelerService.getBolgelerList().subscribe(data => {
			this.bolgelerList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.bolgelerAddForm.valid) {
			this.bolgeler = Object.assign({}, this.bolgelerAddForm.value)

			if (this.bolgeler.bolgelerId == 0)
				this.addBolgeler();
			else
				this.updateBolgeler();
		}

	}

	addBolgeler(){

		this.bolgelerService.addBolgeler(this.bolgeler).subscribe(data => {
			this.getBolgelerList();
			this.bolgeler = new Bolgeler();
			jQuery('#bolgeler').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.bolgelerAddForm);

		})

	}

	updateBolgeler(){

		this.bolgelerService.updateBolgeler(this.bolgeler).subscribe(data => {

			var index=this.bolgelerList.findIndex(x=>x.bolgelerId==this.bolgeler.bolgelerId);
			this.bolgelerList[index]=this.bolgeler;
			this.dataSource = new MatTableDataSource(this.bolgelerList);
            this.configDataTable();
			this.bolgeler = new Bolgeler();
			jQuery('#bolgeler').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.bolgelerAddForm);

		})

	}

	createBolgelerAddForm() {
		this.bolgelerAddForm = this.formBuilder.group({		
			bolgelerId : [0],
ulkeId : [0],
foto : [""],
baslik : [""],
aciklama : [""],
yayin : [0],
sira : [0]
		})
	}

	deleteBolgeler(bolgelerId:number){
		this.bolgelerService.deleteBolgeler(bolgelerId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.bolgelerList=this.bolgelerList.filter(x=> x.bolgelerId!=bolgelerId);
			this.dataSource = new MatTableDataSource(this.bolgelerList);
			this.configDataTable();
		})
	}

	getBolgelerById(bolgelerId:number){
		this.clearFormGroup(this.bolgelerAddForm);
		this.bolgelerService.getBolgelerById(bolgelerId).subscribe(data=>{
			this.bolgeler=data;
			this.bolgelerAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'bolgelerId')
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
