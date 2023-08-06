import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Puan } from './models/Puan';
import { PuanService } from './services/Puan.service';
import { environment } from 'environments/environment';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';

declare var jQuery: any;

@Component({
	selector: 'app-puan',
	templateUrl: './puan.component.html',
	styleUrls: ['./puan.component.scss']
})
export class PuanComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['puanId','rotaId','genelPuan','hizmetler','hizmetlerPuan','konum','konumPuan','kolayliklar','kolayliklarPuan','fiyat','fiyatPuan','yiyecek','yiyecekPuan','harita', 'update','delete'];

	puanList:Puan[];
	puan:Puan=new Puan();
	rotaList:Rota[];
	puanAddForm: FormGroup;


	puanId:number;

	constructor(private rotaService:RotaService,  private puanService:PuanService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getPuanList();
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.createPuanAddForm();
	}


	getPuanList() {
		this.puanService.getPuanList().subscribe(data => {
			this.puanList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.puanAddForm.valid) {
			this.puan = Object.assign({}, this.puanAddForm.value)

			if (this.puan.puanId == 0)
				this.addPuan();
			else
				this.updatePuan();
		}

	}

	addPuan(){

		this.puanService.addPuan(this.puan).subscribe(data => {
			this.getPuanList();
			this.puan = new Puan();
			jQuery('#puan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.puanAddForm);

		})

	}

	updatePuan(){

		this.puanService.updatePuan(this.puan).subscribe(data => {

			var index=this.puanList.findIndex(x=>x.puanId==this.puan.puanId);
			this.puanList[index]=this.puan;
			this.dataSource = new MatTableDataSource(this.puanList);
            this.configDataTable();
			this.puan = new Puan();
			jQuery('#puan').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.puanAddForm);

		})

	}

	createPuanAddForm() {
		this.puanAddForm = this.formBuilder.group({		
			puanId : [0],
rotaId : [0],
genelPuan : [""],
hizmetler : [""],
hizmetlerPuan : [""],
konum : [""],
konumPuan : [""],
kolayliklar : [""],
kolayliklarPuan : [""],
fiyat : [""],
fiyatPuan : [""],
yiyecek : [""],
yiyecekPuan : [""],
harita : [""]
		})
	}

	deletePuan(puanId:number){
		this.puanService.deletePuan(puanId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.puanList=this.puanList.filter(x=> x.puanId!=puanId);
			this.dataSource = new MatTableDataSource(this.puanList);
			this.configDataTable();
		})
	}

	getPuanById(puanId:number){
		this.clearFormGroup(this.puanAddForm);
		this.puanService.getPuanById(puanId).subscribe(data=>{
			this.puan=data;
			this.puanAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'puanId')
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
