import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Kategori } from './models/Kategori';
import { KategoriService } from './services/Kategori.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kategori',
	templateUrl: './kategori.component.html',
	styleUrls: ['./kategori.component.scss']
})
export class KategoriComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['kategoriId','baslik','aciklama','foto','yayin','sira', 'update','delete','file'];

	kategoriList:Kategori[];
	kategori:Kategori=new Kategori();

	kategoriAddForm: FormGroup;
	photoForm: FormGroup;

	kategoriId:number;

	constructor(private kategoriService:KategoriService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKategoriList();
    }

	ngOnInit() {

		this.createKategoriAddForm();
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
		formData.append('kategoriId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.kategoriService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getKategoriList();
				console.log(data);
				this.alertifyService.success(data);
})
	}

	getKategoriList() {
		this.kategoriService.getKategoriList().subscribe(data => {
			this.kategoriList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kategoriAddForm.valid) {
			this.kategori = Object.assign({}, this.kategoriAddForm.value)

			if (this.kategori.kategoriId == 0)
				this.addKategori();
			else
				this.updateKategori();
		}

	}

	addKategori(){

		this.kategoriService.addKategori(this.kategori).subscribe(data => {
			this.getKategoriList();
			this.kategori = new Kategori();
			jQuery('#kategori').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kategoriAddForm);

		})

	}

	updateKategori(){

		this.kategoriService.updateKategori(this.kategori).subscribe(data => {

			var index=this.kategoriList.findIndex(x=>x.kategoriId==this.kategori.kategoriId);
			this.kategoriList[index]=this.kategori;
			this.dataSource = new MatTableDataSource(this.kategoriList);
            this.configDataTable();
			this.kategori = new Kategori();
			jQuery('#kategori').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kategoriAddForm);

		})

	}

	createKategoriAddForm() {
		this.kategoriAddForm = this.formBuilder.group({		
			kategoriId : [0],
baslik : [""],
aciklama : [""],
foto : [""],
yayin : [0],
sira : [0]
		})
	}

	deleteKategori(kategoriId:number){
		this.kategoriService.deleteKategori(kategoriId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kategoriList=this.kategoriList.filter(x=> x.kategoriId!=kategoriId);
			this.dataSource = new MatTableDataSource(this.kategoriList);
			this.configDataTable();
		})
	}

	getKategoriById(kategoriId:number){
		this.clearFormGroup(this.kategoriAddForm);
		this.kategoriService.getKategoriById(kategoriId).subscribe(data=>{
			this.kategori=data;
			this.kategoriAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'kategoriId')
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
