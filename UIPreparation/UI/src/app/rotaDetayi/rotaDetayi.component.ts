import { RotaDetayi } from './models/rotadetayi';
import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';

import { Sehir } from 'app/sehir/models/Sehir';
import { Kategori } from 'app/kategori/models/Kategori';
import { KategoriService } from 'app/kategori/services/Kategori.service';
import { SehirService } from 'app/sehir/services/Sehir.service';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';
import { Router } from '@angular/router';
import { RotaDetayiService } from './services/rotadetayi.service';




declare var jQuery: any;

@Component({
	selector: 'app-rotaDetayi',
	templateUrl: './rotaDetayi.component.html',
	styleUrls: ['./rotaDetayi.component.scss']
})
export class RotaDetayiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['rotaDetayiId','rotaId','baslik','ozet','aciklama','yayin','sira','foto','kategoriId','sehirId', 'update','delete','file'];

	rotaDetayiList:RotaDetayi[];
	rotaDetayi:RotaDetayi=new RotaDetayi();

	rotaDetayiAddForm: FormGroup;
	photoForm: FormGroup;


	rotaList:Rota[];
	sehirList:Sehir[];
	kategoriList:Kategori[];
	rotaDetayiId:number;

	constructor( private router: Router,private rotaService:RotaService, private kategoriService:KategoriService, private sehirService:SehirService, private rotaDetayiService:RotaDetayiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getRotaDetayiList();
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.sehirService.getSehirList().subscribe(data=>this.sehirList=data);
		this.kategoriService.getKategoriList().subscribe(data=>this.kategoriList=data);
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.sehirService.getSehirList().subscribe(data=>this.sehirList=data);
		this.kategoriService.getKategoriList().subscribe(data=>this.kategoriList=data);
		this.createRotaDetayiAddForm();
	}
	navigateToRotaPages(rotaId: number) {
		this.router.navigate(['/rotapages', rotaId]);
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
		formData.append('rotaDetayiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.rotaDetayiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getRotaDetayiList();
				console.log(data);
				this.alertifyService.success(data);
})
	}



	getRotaDetayiList() {
		this.rotaDetayiService.getRotaDetayiList().subscribe(data => {
			this.rotaDetayiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.rotaDetayiAddForm.valid) {
			this.rotaDetayi = Object.assign({}, this.rotaDetayiAddForm.value)

			if (this.rotaDetayi.rotaDetayiId == 0)
				this.addRotaDetayi();
			else
				this.updateRotaDetayi();
		}

	}

	addRotaDetayi(){

		this.rotaDetayiService.addRotaDetayi(this.rotaDetayi).subscribe(data => {
			this.getRotaDetayiList();
			this.rotaDetayi = new RotaDetayi();
			jQuery('#rotadetayi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaDetayiAddForm);

		})

	}

	updateRotaDetayi(){

		this.rotaDetayiService.updateRotaDetayi(this.rotaDetayi).subscribe(data => {

			var index=this.rotaDetayiList.findIndex(x=>x.rotaDetayiId==this.rotaDetayi.rotaDetayiId);
			this.rotaDetayiList[index]=this.rotaDetayi;
			this.dataSource = new MatTableDataSource(this.rotaDetayiList);
            this.configDataTable();
			this.rotaDetayi = new RotaDetayi();
			jQuery('#rotadetayi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaDetayiAddForm);

		})

	}

	createRotaDetayiAddForm() {
		this.rotaDetayiAddForm = this.formBuilder.group({		
			rotaDetayiId : [0],
rotaId : [0],
baslik : [""],
ozet : [""],
aciklama : [""],
yayin : [""],
sira : [0],
foto : [""],
kategoriId : [0],
sehirId : [0]
		})
	}

	deleteRotaDetayi(rotaDetayiId:number){
		this.rotaDetayiService.deleteRotaDetayi(rotaDetayiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.rotaDetayiList=this.rotaDetayiList.filter(x=> x.rotaDetayiId!=rotaDetayiId);
			this.dataSource = new MatTableDataSource(this.rotaDetayiList);
			this.configDataTable();
		})
	}

	getRotaDetayiById(rotaDetayiId:number){
		this.clearFormGroup(this.rotaDetayiAddForm);
		this.rotaDetayiService.getRotaDetayiById(rotaDetayiId).subscribe(data=>{
			this.rotaDetayi=data;
			this.rotaDetayiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'rotaDetayiId')
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
