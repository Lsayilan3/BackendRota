import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Sehir } from './models/Sehir';
import { SehirService } from './services/Sehir.service';
import { environment } from 'environments/environment';
import { Bolgeler } from 'app/bolgeler/models/Bolgeler';
import { BolgelerService } from 'app/bolgeler/services/Bolgeler.service';
import { UlkeService } from 'app/ulke/services/Ulke.service';
import { Ulke } from 'app/ulke/models/Ulke';

declare var jQuery: any;

@Component({
	selector: 'app-sehir',
	templateUrl: './sehir.component.html',
	styleUrls: ['./sehir.component.scss']
})
export class SehirComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['sehirId','bolgelerId','foto','baslik','aciklama','yayin','sira', 'update','delete','file'];

	sehirList:Sehir[];
	sehir:Sehir=new Sehir();

	sehirAddForm: FormGroup;
	photoForm: FormGroup;


	bolgelerList:Bolgeler[];
	ulkeList:Ulke[];

	sehirId:number;

	constructor( private ulkeService:UlkeService,private bolgelerService:BolgelerService, private sehirService:SehirService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getSehirList();
		this.ulkeService.getUlkeList().subscribe(data=>this.ulkeList=data);
		this.bolgelerService.getBolgelerList().subscribe(data=>this.bolgelerList=data);
    }

	ngOnInit() {
		this.ulkeService.getUlkeList().subscribe(data=>this.ulkeList=data);
		this.bolgelerService.getBolgelerList().subscribe(data=>this.bolgelerList=data);
		this.createSehirAddForm();
	}

	onUlkeChange(ulkeId: any) {
		this.bolgelerService.getBolgelerByUlkeId(ulkeId).subscribe(data => {
		  this.bolgelerList = data;
		});
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
		formData.append('sehirId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.sehirService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getSehirList();
				console.log(data);
				this.alertifyService.success(data);
})
	}


	getSehirList() {
		this.sehirService.getSehirList().subscribe(data => {
			this.sehirList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sehirAddForm.valid) {
			this.sehir = Object.assign({}, this.sehirAddForm.value)

			if (this.sehir.sehirId == 0)
				this.addSehir();
			else
				this.updateSehir();
		}

	}

	addSehir(){

		this.sehirService.addSehir(this.sehir).subscribe(data => {
			this.getSehirList();
			this.sehir = new Sehir();
			jQuery('#sehir').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sehirAddForm);

		})

	}

	updateSehir(){

		this.sehirService.updateSehir(this.sehir).subscribe(data => {

			var index=this.sehirList.findIndex(x=>x.sehirId==this.sehir.sehirId);
			this.sehirList[index]=this.sehir;
			this.dataSource = new MatTableDataSource(this.sehirList);
            this.configDataTable();
			this.sehir = new Sehir();
			jQuery('#sehir').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sehirAddForm);

		})

	}

	createSehirAddForm() {
		this.sehirAddForm = this.formBuilder.group({		
			sehirId : [0],
bolgelerId : [0],
foto : [""],
baslik : [""],
aciklama : [""],
yayin : [0],
sira : [0]
		})
	}

	deleteSehir(sehirId:number){
		this.sehirService.deleteSehir(sehirId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sehirList=this.sehirList.filter(x=> x.sehirId!=sehirId);
			this.dataSource = new MatTableDataSource(this.sehirList);
			this.configDataTable();
		})
	}

	getSehirById(sehirId:number){
		this.clearFormGroup(this.sehirAddForm);
		this.sehirService.getSehirById(sehirId).subscribe(data=>{
			this.sehir=data;
			this.sehirAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'sehirId')
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
