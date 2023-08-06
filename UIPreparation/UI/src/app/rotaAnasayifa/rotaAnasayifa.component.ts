import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { RotaAnasayifa } from './models/RotaAnasayifa';
import { RotaAnasayifaService } from './services/RotaAnasayifa.service';
import { environment } from 'environments/environment';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';

declare var jQuery: any;

@Component({
	selector: 'app-rotaAnasayifa',
	templateUrl: './rotaAnasayifa.component.html',
	styleUrls: ['./rotaAnasayifa.component.scss']
})
export class RotaAnasayifaComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['rotaAnasayifaId','rotaId','foto','baslik','aciklama','col','yayin','sira', 'update','delete','file'];

	rotaAnasayifaList:RotaAnasayifa[];
	rotaAnasayifa:RotaAnasayifa=new RotaAnasayifa();

	rotaAnasayifaAddForm: FormGroup;

	rotaList:Rota[];
	photoForm: FormGroup;
	rotaAnasayifaId:number;

	constructor( private rotaService:RotaService, private rotaAnasayifaService:RotaAnasayifaService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRotaAnasayifaList();
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.getRotaAnasayifaList();
		this.createRotaAnasayifaAddForm();
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
		formData.append('rotaAnasayifaId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.rotaAnasayifaService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getRotaAnasayifaList();
				console.log(data);
				this.alertifyService.success(data);
})
	}


	getRotaAnasayifaList() {
		this.rotaAnasayifaService.getRotaAnasayifaList().subscribe(data => {
			this.rotaAnasayifaList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rotaAnasayifaAddForm.valid) {
			this.rotaAnasayifa = Object.assign({}, this.rotaAnasayifaAddForm.value)

			if (this.rotaAnasayifa.rotaAnasayifaId == 0)
				this.addRotaAnasayifa();
			else
				this.updateRotaAnasayifa();
		}

	}

	addRotaAnasayifa(){

		this.rotaAnasayifaService.addRotaAnasayifa(this.rotaAnasayifa).subscribe(data => {
			this.getRotaAnasayifaList();
			this.rotaAnasayifa = new RotaAnasayifa();
			jQuery('#rotaanasayifa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaAnasayifaAddForm);

		})

	}

	updateRotaAnasayifa(){

		this.rotaAnasayifaService.updateRotaAnasayifa(this.rotaAnasayifa).subscribe(data => {

			var index=this.rotaAnasayifaList.findIndex(x=>x.rotaAnasayifaId==this.rotaAnasayifa.rotaAnasayifaId);
			this.rotaAnasayifaList[index]=this.rotaAnasayifa;
			this.dataSource = new MatTableDataSource(this.rotaAnasayifaList);
            this.configDataTable();
			this.rotaAnasayifa = new RotaAnasayifa();
			jQuery('#rotaanasayifa').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaAnasayifaAddForm);

		})

	}

	createRotaAnasayifaAddForm() {
		this.rotaAnasayifaAddForm = this.formBuilder.group({		
			rotaAnasayifaId : [0],
rotaId : [0],
foto : [""],
baslik : [""],
aciklama : [""],
col : [""],
yayin : [0],
sira : [0]
		})
	}

	deleteRotaAnasayifa(rotaAnasayifaId:number){
		this.rotaAnasayifaService.deleteRotaAnasayifa(rotaAnasayifaId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rotaAnasayifaList=this.rotaAnasayifaList.filter(x=> x.rotaAnasayifaId!=rotaAnasayifaId);
			this.dataSource = new MatTableDataSource(this.rotaAnasayifaList);
			this.configDataTable();
		})
	}

	getRotaAnasayifaById(rotaAnasayifaId:number){
		this.clearFormGroup(this.rotaAnasayifaAddForm);
		this.rotaAnasayifaService.getRotaAnasayifaById(rotaAnasayifaId).subscribe(data=>{
			this.rotaAnasayifa=data;
			this.rotaAnasayifaAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'rotaAnasayifaId')
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
