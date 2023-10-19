import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Rota } from './models/Rota';

import { environment } from 'environments/environment';
import { RotaService } from './services/Rota.service';
import { SehirService } from 'app/sehir/services/Sehir.service';
import { Sehir } from 'app/sehir/models/Sehir';
import { Kategori } from 'app/kategori/models/Kategori';
import { KategoriService } from 'app/kategori/services/Kategori.service';
import { Router } from '@angular/router';

declare var jQuery: any;

@Component({
  selector: 'app-rota',
  templateUrl: './rota.component.html',
  styleUrls: ['./rota.component.scss']
})
export class RotaComponent implements AfterViewInit, OnInit {
  
  dataSource: MatTableDataSource<Rota>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = ['rotaId','baslik','ozet','aciklama','yayin','sira','foto','kategoriId','sehirId','anaRotaId', 'update','delete', 'search','file'];

  rotaList: Rota[];
  rota: Rota = new Rota();

  rotaAddForm: FormGroup;
  photoForm: FormGroup;


  sehirList: Sehir[];
  kategoriList: Kategori[];
  rotaId: number;

  constructor(
    private kategoriService: KategoriService,
    private sehirService: SehirService,
    private rotaService: RotaService,
    private lookupService: LookUpService,
    private alertifyService: AlertifyService,
    private formBuilder: FormBuilder,
    private authService: AuthService,
	private router: Router,
  ) {}

  ngAfterViewInit(): void {
    this.getRotaList();
    this.sehirService.getSehirList().subscribe(data => (this.sehirList = data));
    this.kategoriService.getKategoriList().subscribe(data => (this.kategoriList = data));
  }

  ngOnInit() {
    this.sehirService.getSehirList().subscribe(data => (this.sehirList = data));
    this.kategoriService.getKategoriList().subscribe(data => (this.kategoriList = data));
    this.createRotaAddForm();
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
		formData.append('rotaId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.rotaService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getRotaList();
				console.log(data);
				this.alertifyService.success(data);
})
	}
  getRotaList() {
    this.rotaService.getRotaList().subscribe(data => {
      this.rotaList = data;
      this.dataSource = new MatTableDataSource<Rota>(data);
      this.configDataTable();
    });
  }

  save() {
    if (this.rotaAddForm.valid) {
      this.rota = Object.assign({}, this.rotaAddForm.value);
      if (this.rota.rotaId === 0) {
        this.addRota();
      } else {
        this.updateRota();
      }
    }
  }

  addRota() {
    this.rotaService.addRota(this.rota).subscribe(data => {
      this.getRotaList();
      this.rota = new Rota();
      jQuery('#rota').modal('hide');
      this.alertifyService.success(data);
      this.clearFormGroup(this.rotaAddForm);
    });
  }

  updateRota() {
    this.rotaService.updateRota(this.rota).subscribe(data => {
      const index = this.rotaList.findIndex(x => x.rotaId === this.rota.rotaId);
      this.rotaList[index] = this.rota;
      this.dataSource = new MatTableDataSource<Rota>(this.rotaList);
      this.configDataTable();
      this.rota = new Rota();
      jQuery('#rota').modal('hide');
      this.alertifyService.success(data);
      this.clearFormGroup(this.rotaAddForm);
    });
  }

  createRotaAddForm() {
    this.rotaAddForm = this.formBuilder.group({
      rotaId: [0],
      baslik: [''],
      ozet: [''],
      aciklama: [''],
      yayin: [''],
      sira: [0],
      foto: [''],
      kategoriId: [0],
      sehirId: [0],
      anaRotaId: [0]
    });
  }

  deleteRota(rotaId: number) {
    this.rotaService.deleteRota(rotaId).subscribe(data => {
      this.alertifyService.success(data.toString());
      this.rotaList = this.rotaList.filter(x => x.rotaId !== rotaId);
      this.dataSource = new MatTableDataSource<Rota>(this.rotaList);
      this.configDataTable();
    });
  }

  getRotaById(rotaId: number) {
    this.clearFormGroup(this.rotaAddForm);
    this.rotaService.getRotaById(rotaId).subscribe(data => {
      this.rota = data;
      this.rotaAddForm.patchValue(data);
    });
  }

  clearFormGroup(group: FormGroup) {
    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
      group.get(key).setErrors(null);
      if (key === 'rotaId') {
        group.get(key).setValue(0);
      }
    });
  }

  checkClaim(claim: string): boolean {
    return this.authService.claimGuard(claim);
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
