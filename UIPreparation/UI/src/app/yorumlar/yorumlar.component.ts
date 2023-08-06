import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Yorumlar } from './models/Yorumlar';
import { YorumlarService } from './services/Yorumlar.service';
import { environment } from 'environments/environment';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';

declare var jQuery: any;

@Component({
	selector: 'app-yorumlar',
	templateUrl: './yorumlar.component.html',
	styleUrls: ['./yorumlar.component.scss']
})
export class YorumlarComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['yorumlarId','rotaId','puan','isim','baslik','yorum','yayin', 'update','delete'];

	yorumlarList:Yorumlar[];
	yorumlar:Yorumlar=new Yorumlar();

	yorumlarAddForm: FormGroup;

	rotaList:Rota[];
	yorumlarId:number;

	constructor(private rotaService:RotaService, private yorumlarService:YorumlarService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getYorumlarList();
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
    }

	ngOnInit() {
		this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.createYorumlarAddForm();
	}


	getYorumlarList() {
		this.yorumlarService.getYorumlarList().subscribe(data => {
			this.yorumlarList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.yorumlarAddForm.valid) {
			this.yorumlar = Object.assign({}, this.yorumlarAddForm.value)

			if (this.yorumlar.yorumlarId == 0)
				this.addYorumlar();
			else
				this.updateYorumlar();
		}

	}

	addYorumlar(){

		this.yorumlarService.addYorumlar(this.yorumlar).subscribe(data => {
			this.getYorumlarList();
			this.yorumlar = new Yorumlar();
			jQuery('#yorumlar').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yorumlarAddForm);

		})

	}

	updateYorumlar(){

		this.yorumlarService.updateYorumlar(this.yorumlar).subscribe(data => {

			var index=this.yorumlarList.findIndex(x=>x.yorumlarId==this.yorumlar.yorumlarId);
			this.yorumlarList[index]=this.yorumlar;
			this.dataSource = new MatTableDataSource(this.yorumlarList);
            this.configDataTable();
			this.yorumlar = new Yorumlar();
			jQuery('#yorumlar').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.yorumlarAddForm);

		})

	}

	createYorumlarAddForm() {
		this.yorumlarAddForm = this.formBuilder.group({		
			yorumlarId : [0],
rotaId : [0],
puan : [0],
isim : [""],
baslik : [""],
yorum : [""],
yayin : [0]
		})
	}

	deleteYorumlar(yorumlarId:number){
		this.yorumlarService.deleteYorumlar(yorumlarId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.yorumlarList=this.yorumlarList.filter(x=> x.yorumlarId!=yorumlarId);
			this.dataSource = new MatTableDataSource(this.yorumlarList);
			this.configDataTable();
		})
	}

	getYorumlarById(yorumlarId:number){
		this.clearFormGroup(this.yorumlarAddForm);
		this.yorumlarService.getYorumlarById(yorumlarId).subscribe(data=>{
			this.yorumlar=data;
			this.yorumlarAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'yorumlarId')
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
