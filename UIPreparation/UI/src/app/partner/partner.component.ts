import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Partner } from './models/Partner';
import { PartnerService } from './services/Partner.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-partner',
	templateUrl: './partner.component.html',
	styleUrls: ['./partner.component.scss']
})
export class PartnerComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['partnerId','foto', 'update','delete','file'];

	partnerList:Partner[];
	partner:Partner=new Partner();

	partnerAddForm: FormGroup;
	photoForm: FormGroup;

	partnerId:number;

	constructor(private partnerService:PartnerService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getPartnerList();
    }

	ngOnInit() {

		this.createPartnerAddForm();
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
		formData.append('partnerId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.partnerService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getPartnerList();
				console.log(data);
				this.alertifyService.success(data);
})
	}

	getPartnerList() {
		this.partnerService.getPartnerList().subscribe(data => {
			this.partnerList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.partnerAddForm.valid) {
			this.partner = Object.assign({}, this.partnerAddForm.value)

			if (this.partner.partnerId == 0)
				this.addPartner();
			else
				this.updatePartner();
		}

	}

	addPartner(){

		this.partnerService.addPartner(this.partner).subscribe(data => {
			this.getPartnerList();
			this.partner = new Partner();
			jQuery('#partner').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.partnerAddForm);

		})

	}

	updatePartner(){

		this.partnerService.updatePartner(this.partner).subscribe(data => {

			var index=this.partnerList.findIndex(x=>x.partnerId==this.partner.partnerId);
			this.partnerList[index]=this.partner;
			this.dataSource = new MatTableDataSource(this.partnerList);
            this.configDataTable();
			this.partner = new Partner();
			jQuery('#partner').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.partnerAddForm);

		})

	}

	createPartnerAddForm() {
		this.partnerAddForm = this.formBuilder.group({		
			partnerId : [0],
foto : ["", Validators.required]
		})
	}

	deletePartner(partnerId:number){
		this.partnerService.deletePartner(partnerId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.partnerList=this.partnerList.filter(x=> x.partnerId!=partnerId);
			this.dataSource = new MatTableDataSource(this.partnerList);
			this.configDataTable();
		})
	}

	getPartnerById(partnerId:number){
		this.clearFormGroup(this.partnerAddForm);
		this.partnerService.getPartnerById(partnerId).subscribe(data=>{
			this.partner=data;
			this.partnerAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'partnerId')
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
