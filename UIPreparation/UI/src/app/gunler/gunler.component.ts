import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Gunler } from './models/Gunler';
import { GunlerService } from './services/Gunler.service';
import { environment } from 'environments/environment';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';

declare var jQuery: any;

@Component({
	selector: 'app-gunler',
	templateUrl: './gunler.component.html',
	styleUrls: ['./gunler.component.scss']
})
export class GunlerComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['gunlerId','rotaId','baslik','aciklama', 'update','delete'];

	gunlerList:Gunler[];
	gunler:Gunler=new Gunler();

	gunlerAddForm: FormGroup;
	rotaList:Rota[];

	gunlerId:number;

	constructor(private rotaService:RotaService,private gunlerService:GunlerService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getGunlerList();
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.createGunlerAddForm();
	}


	getGunlerList() {
		this.gunlerService.getGunlerList().subscribe(data => {
			this.gunlerList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.gunlerAddForm.valid) {
			this.gunler = Object.assign({}, this.gunlerAddForm.value)

			if (this.gunler.gunlerId == 0)
				this.addGunler();
			else
				this.updateGunler();
		}

	}

	addGunler(){

		this.gunlerService.addGunler(this.gunler).subscribe(data => {
			this.getGunlerList();
			this.gunler = new Gunler();
			jQuery('#gunler').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.gunlerAddForm);

		})

	}

	updateGunler(){

		this.gunlerService.updateGunler(this.gunler).subscribe(data => {

			var index=this.gunlerList.findIndex(x=>x.gunlerId==this.gunler.gunlerId);
			this.gunlerList[index]=this.gunler;
			this.dataSource = new MatTableDataSource(this.gunlerList);
            this.configDataTable();
			this.gunler = new Gunler();
			jQuery('#gunler').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.gunlerAddForm);

		})

	}

	createGunlerAddForm() {
		this.gunlerAddForm = this.formBuilder.group({		
			gunlerId : [0],
rotaId : [0],
baslik : [""],
aciklama : [""]
		})
	}

	deleteGunler(gunlerId:number){
		this.gunlerService.deleteGunler(gunlerId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.gunlerList=this.gunlerList.filter(x=> x.gunlerId!=gunlerId);
			this.dataSource = new MatTableDataSource(this.gunlerList);
			this.configDataTable();
		})
	}

	getGunlerById(gunlerId:number){
		this.clearFormGroup(this.gunlerAddForm);
		this.gunlerService.getGunlerById(gunlerId).subscribe(data=>{
			this.gunler=data;
			this.gunlerAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'gunlerId')
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
