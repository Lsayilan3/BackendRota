import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { ResimTipi } from './models/ResimTipi';
import { ResimTipiService } from './services/ResimTipi.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-resimTipi',
	templateUrl: './resimTipi.component.html',
	styleUrls: ['./resimTipi.component.scss']
})
export class ResimTipiComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['resimTipiId','adi', 'update','delete'];

	resimTipiList:ResimTipi[];
	resimTipi:ResimTipi=new ResimTipi();

	resimTipiAddForm: FormGroup;


	resimTipiId:number;

	constructor(private resimTipiService:ResimTipiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getResimTipiList();
    }

	ngOnInit() {

		this.createResimTipiAddForm();
	}


	getResimTipiList() {
		this.resimTipiService.getResimTipiList().subscribe(data => {
			this.resimTipiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.resimTipiAddForm.valid) {
			this.resimTipi = Object.assign({}, this.resimTipiAddForm.value)

			if (this.resimTipi.resimTipiId == 0)
				this.addResimTipi();
			else
				this.updateResimTipi();
		}

	}

	addResimTipi(){

		this.resimTipiService.addResimTipi(this.resimTipi).subscribe(data => {
			this.getResimTipiList();
			this.resimTipi = new ResimTipi();
			jQuery('#resimtipi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.resimTipiAddForm);

		})

	}

	updateResimTipi(){

		this.resimTipiService.updateResimTipi(this.resimTipi).subscribe(data => {

			var index=this.resimTipiList.findIndex(x=>x.resimTipiId==this.resimTipi.resimTipiId);
			this.resimTipiList[index]=this.resimTipi;
			this.dataSource = new MatTableDataSource(this.resimTipiList);
            this.configDataTable();
			this.resimTipi = new ResimTipi();
			jQuery('#resimtipi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.resimTipiAddForm);

		})

	}

	createResimTipiAddForm() {
		this.resimTipiAddForm = this.formBuilder.group({		
			resimTipiId : [0],
adi : [""]
		})
	}

	deleteResimTipi(resimTipiId:number){
		this.resimTipiService.deleteResimTipi(resimTipiId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.resimTipiList=this.resimTipiList.filter(x=> x.resimTipiId!=resimTipiId);
			this.dataSource = new MatTableDataSource(this.resimTipiList);
			this.configDataTable();
		})
	}

	getResimTipiById(resimTipiId:number){
		this.clearFormGroup(this.resimTipiAddForm);
		this.resimTipiService.getResimTipiById(resimTipiId).subscribe(data=>{
			this.resimTipi=data;
			this.resimTipiAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'resimTipiId')
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
