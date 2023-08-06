import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { RotaGaleri } from './models/RotaGaleri';
import { RotaGaleriService } from './services/RotaGaleri.service';
import { environment } from 'environments/environment';
import { RotaService } from 'app/rota/services/Rota.service';
import { Rota } from 'app/rota/models/Rota';
import { ResimTipiService } from 'app/resimTipi/services/ResimTipi.service';
import { ResimTipi } from 'app/resimTipi/models/ResimTipi';

declare var jQuery: any;

@Component({
	selector: 'app-rotaGaleri',
	templateUrl: './rotaGaleri.component.html',
	styleUrls: ['./rotaGaleri.component.scss']
})
export class RotaGaleriComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['rotaGaleriId','rotaId','foto','baslik','aciklama','yayin','resimTipiId', 'update','delete','file'];

	rotaGaleriList:RotaGaleri[];
	rotaGaleri:RotaGaleri=new RotaGaleri();

	rotaGaleriAddForm: FormGroup;
	photoForm: FormGroup;

	rotaGaleriId:number;

	rotaList:Rota[];

	resimTipiList:ResimTipi[];
	resimTipi:ResimTipi=new ResimTipi();


	constructor(private resimTipiService:ResimTipiService, private rotaService:RotaService, private rotaGaleriService:RotaGaleriService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRotaGaleriList();
		this.resimTipiService.getResimTipiList().subscribe(data=>this.resimTipiList=data);
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.createRotaGaleriAddForm();
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
		formData.append('rotaGaleriId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.rotaGaleriService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getRotaGaleriList();
				console.log(data);
				this.alertifyService.success(data);
})
	}


	getRotaGaleriList() {
		this.rotaGaleriService.getRotaGaleriList().subscribe(data => {
			this.rotaGaleriList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rotaGaleriAddForm.valid) {
			this.rotaGaleri = Object.assign({}, this.rotaGaleriAddForm.value)

			if (this.rotaGaleri.rotaGaleriId == 0)
				this.addRotaGaleri();
			else
				this.updateRotaGaleri();
		}

	}

	addRotaGaleri(){

		this.rotaGaleriService.addRotaGaleri(this.rotaGaleri).subscribe(data => {
			this.getRotaGaleriList();
			this.rotaGaleri = new RotaGaleri();
			jQuery('#rotagaleri').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaGaleriAddForm);

		})

	}

	updateRotaGaleri(){

		this.rotaGaleriService.updateRotaGaleri(this.rotaGaleri).subscribe(data => {

			var index=this.rotaGaleriList.findIndex(x=>x.rotaGaleriId==this.rotaGaleri.rotaGaleriId);
			this.rotaGaleriList[index]=this.rotaGaleri;
			this.dataSource = new MatTableDataSource(this.rotaGaleriList);
            this.configDataTable();
			this.rotaGaleri = new RotaGaleri();
			jQuery('#rotagaleri').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaGaleriAddForm);

		})

	}

	createRotaGaleriAddForm() {
		this.rotaGaleriAddForm = this.formBuilder.group({		
			rotaGaleriId : [0],
rotaId : [0],
foto : [""],
baslik : [""],
aciklama : [""],
yayin : [0],
resimTipiId : [0]
		})
	}

	deleteRotaGaleri(rotaGaleriId:number){
		this.rotaGaleriService.deleteRotaGaleri(rotaGaleriId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rotaGaleriList=this.rotaGaleriList.filter(x=> x.rotaGaleriId!=rotaGaleriId);
			this.dataSource = new MatTableDataSource(this.rotaGaleriList);
			this.configDataTable();
		})
	}

	getRotaGaleriById(rotaGaleriId:number){
		this.clearFormGroup(this.rotaGaleriAddForm);
		this.rotaGaleriService.getRotaGaleriById(rotaGaleriId).subscribe(data=>{
			this.rotaGaleri=data;
			this.rotaGaleriAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'rotaGaleriId')
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
