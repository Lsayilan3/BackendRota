import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RotaDetayiService } from './../rotaDetayi/services/rotadetayi.service';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Rota } from 'app/rota/models/Rota';
import { RotaService } from 'app/rota/services/Rota.service';
import { RotaDetayi } from '../rotaDetayi/models/rotadetayi';
import { RotaGaleri } from 'app/rotaGaleri/models/RotaGaleri';
import { SehirService } from 'app/sehir/services/Sehir.service';
import { KategoriService } from 'app/kategori/services/Kategori.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { Sehir } from 'app/sehir/models/Sehir';
import { Kategori } from 'app/kategori/models/Kategori';
import { environment } from 'environments/environment';
import { RotaGaleriService } from 'app/rotaGaleri/services/RotaGaleri.service';
import { ResimTipi } from 'app/resimTipi/models/ResimTipi';
import { ResimTipiService } from 'app/resimTipi/services/ResimTipi.service';







declare var jQuery: any;

@Component({
  selector: 'app-rotaPages',
  templateUrl: './rotaPages.component.html',
  styleUrls: ['./rotaPages.component.scss']
})
export class RotaPagesComponent implements OnInit {
  dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['rotaDetayiId', 'rotaId','baslik','ozet','aciklama','yayin','sira','foto','kategoriId','sehirId', 'update','delete','file'];


// Diğer kodlar

  rotaDetayis: RotaDetayi[] = [];
  rotaGaleris: RotaGaleri[] = [];
  rotaDetayiList:RotaDetayi[];
	rotaDetayi:RotaDetayi=new RotaDetayi();

  baseUrl :string=environment.getApiUrlPhoto;
  
  rotaGaleriList: RotaGaleri[] = []; // veya başlangıç verisiyle doldurulmuş bir dizi


	rotaDetayiAddForm: FormGroup;
  rotaGaleriForm: FormGroup;
  photoForm: FormGroup;
  rota: Rota = new Rota();
  rotaId: number; // RotaId özelliğini tanımlayın
  rotaGaleriAddForm: FormGroup;

	rotaList:Rota[];
	sehirList:Sehir[];
	kategoriList:Kategori[];

  rotaGaleriId: number ;

  rotaGaleri: RotaGaleri = new RotaGaleri();
  modalBaslik: string = '';
  updateForm: FormGroup;


  resimTipiList:ResimTipi[];
	resimTipi:ResimTipi=new ResimTipi();

  editorContent: string = '';


  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private rotaService: RotaService,
    private authService: AuthService,
    private resimTipiService:ResimTipiService, private rotaGaleriService:RotaGaleriService,  private kategoriService:KategoriService, private sehirService:SehirService, private rotaDetayiService:RotaDetayiService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, 
   
  ) {}

  ngAfterViewInit(): void {
    this.getRotaDetayById(this.rota.rotaId);
    this.resimTipiService.getResimTipiList().subscribe(data=>this.resimTipiList=data);
this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
this.sehirService.getSehirList().subscribe(data=>this.sehirList=data);
this.kategoriService.getKategoriList().subscribe(data=>this.kategoriList=data);
}

  ngOnInit() {
    this.getRotaDetayById(this.rota.rotaId);
    this.updateForm = this.formBuilder.group({
      rotaGaleriId: 0,
      rotaId: 0,
      foto: [''],
      baslik: [''],
      aciklama: [''],
      yayin: 0,
      resimTipiId: 0,
    });
    this.upFile(this.rotaGaleri.rotaGaleriId);
    this.getRotaGaleriById(this.rotaGaleri.rotaId);
    this.createRotaGaleriAddForm();
    this.route.params.subscribe((params) => {
      const rotaId = params['rotaId'];
      this.getRotaById(rotaId);
      this.getRotaDetayById(rotaId);
      this.getRotaGaleriById(rotaId);
    });
    this.createRotaDetayiAddForm();
    this.rotaService.getRotaList().subscribe(data=>this.rotaList=data);
		this.sehirService.getSehirList().subscribe(data=>this.sehirList=data);
		this.kategoriService.getKategoriList().subscribe(data=>this.kategoriList=data);
  }


  uploadFilee(event) {
		const file = (event.target as HTMLInputElement).files[0];
		this.photoForm.patchValue({
		  file: file,
		});
		this.photoForm.get('file').updateValueAndValidity();
		
	  }

	upFilee( id : number){
		this.photoForm = this.formBuilder.group({		
			id : [id],
file : ["", Validators.required]
		})
	}

	addPhotoSavee(){
		var formData: any = new FormData();
		formData.append('rotaDetayiId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

this.rotaDetayiService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
        this.getRotaDetayById(this.rota.rotaId);
				console.log(data);
				this.alertifyService.success(data);
})
	}

  

  getRotaById(rotaId: number) {
    this.rotaService.getRotaById(rotaId).subscribe(
      (rota: Rota) => {
        this.rota = rota;
      },
      (error) => {
        // Hata yönetimi
      }
    );
  }
  getRotaDetayById(rotaId: number) {
    this.rotaService.getRotaDetayById(rotaId).subscribe(
      (rotaDetayis: RotaDetayi[]) => {
        this.rotaDetayis = rotaDetayis;
        this.dataSource = new MatTableDataSource(rotaDetayis);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.rotaDetayis = []; // Hata durumunda rotaDetay dizisini boş olarak ayarlayın
      }
    );
  }
  
  

  getRotaGaleriById(rotaId: number) {
    this.rotaService.getRotaGaleriById(rotaId).subscribe(
      (rotaGaleris: RotaGaleri[]) => {
        this.rotaGaleris = rotaGaleris;
      },
      (error) => {
        // Hata yönetimi
      }
    );
  }


  navigateToRotaPages(rotaId: number) {
    this.router.navigate(['/rotapages', rotaId]);
  }
	getRotaDetayiList() {
		this.rotaDetayiService.getRotaDetayiList().subscribe(data => {
			this.rotaDetayiList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

  openModall() {
    this.rotaDetayi = new RotaDetayi();
    this.rotaDetayiAddForm.patchValue({
      rotaId: this.rota.rotaId
    });
    jQuery('#rotadetayi').modal('show');
  }

  save() {
    if (this.rotaDetayiAddForm.valid) {
      this.rotaDetayi = { ...this.rotaDetayiAddForm.value, rotaId: this.rota.rotaId };
  
      if (this.rotaDetayi.rotaDetayiId == 0)
        this.addRotaDetayi();
      else
        this.updateRotaDetayi();
    }
  }
  

	addRotaDetayi(){

		this.rotaDetayiService.addRotaDetayi(this.rotaDetayi).subscribe(data => {
      this.getRotaDetayById(this.rota.rotaId);
			this.rotaDetayi = new RotaDetayi();
			jQuery('#rotadetayi').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.rotaDetayiAddForm);

		})

	}

  updateRotaDetayi() {
    this.rotaDetayiService.updateRotaDetayi(this.rotaDetayi).subscribe(
      (data) => {
        const index = this.rotaDetayis.findIndex((x) => x.rotaDetayiId === this.rotaDetayi.rotaDetayiId);
        this.rotaDetayis[index] = { ...this.rotaDetayi }; // Güncellenen rotaDetayi nesnesini dizideki ilgili örnekle değiştirin
        this.dataSource = new MatTableDataSource(this.rotaDetayis);
        this.configDataTable();
        this.rotaDetayi = new RotaDetayi();
        jQuery('#rotadetayi').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.rotaDetayiAddForm);
        this.getRotaDetayById(this.rota.rotaId);
      },
      
      (error) => {
        console.error('Rota detayı güncelleme hatası:', error);
      }
    );
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

  deleteRotaDetayi(rotaDetayiId: number) {
    if (confirm('Rota detayını silmek istediğinize emin misiniz?')) {
      this.rotaDetayiService.deleteRotaDetayi(rotaDetayiId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.rotaDetayis = this.rotaDetayis.filter((x) => x.rotaDetayiId !== rotaDetayiId);
          this.dataSource = new MatTableDataSource(this.rotaDetayis);
          this.configDataTable();
        },
        (error) => {
          console.error('Rota detayı silme hatası:', error);
        }
      );
    }
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




  // ROTA GALERİ 

  openModalll() {
    this.rotaDetayi = new RotaDetayi();
    this.rotaDetayiAddForm.patchValue({
      rotaId: this.rota.rotaId
    });
    jQuery('#rotagaleri').modal('show');
  }
  

  savee() {
    if (this.rotaGaleriAddForm.valid) {
      const formData = {
        ...this.rotaGaleriAddForm.value,
        rotaId: this.rota.rotaId
      };
      this.rotaGaleriService.addRotaGaleri(formData).subscribe(
        (response) => {
          // Ekleme işlemi başarılı
          this.alertifyService.success('Rota galerisi başarıyla eklendi.');
          this.clearFormGroup(this.rotaGaleriAddForm);
          jQuery('#rotagaleri').modal('hide');
          this.getRotaGaleriById(this.rota.rotaId);
        },
        (error) => {
          // Ekleme işlemi sırasında hata oluştu
          this.alertifyService.error('Rota galerisi eklenirken bir hata oluştu.');
        }
      );
    } else {
      // Form geçerli değil, gerekli alanları doldurun
      this.alertifyService.error('Lütfen tüm gerekli alanları doldurun.');
    }
  }
  

  deleteResim(rotaGaleri: RotaGaleri) {
    if (confirm('Resmi silmek istediğinize emin misiniz?')) {
      this.rotaGaleriService.deleteRotaGaleri(rotaGaleri.rotaGaleriId).subscribe(
        (response) => {
          // İstek başarılı olduğunda yapılacak işlemler
          console.log('Resim silme başarılı.');
          // Silinen resmi güncellemek için yeniden çekme işlemi
          this.getRotaGaleriById(this.rota.rotaId);
        },
        (error) => {
          // İstek hatalı olduğunda yapılacak işlemler
          console.error('Resim silme hatası:', error);
        }
      );
    }
  }

  createRotaGaleriAddForm() {
    this.rotaGaleriAddForm = this.formBuilder.group({
      rotaGaleriId: [0],
      rotaId: [0],
      foto: [""],
      baslik: [""],
      aciklama: [""],
      yayin: [0],
      resimTipiId: [0]
    });
  }

  
// ..........................................



// Component dosyanızın içinde bulunan ilgili sınıfın içindeki fonksiyonu güncelleyin

uploadFile(event) {
  const file = (event.target as HTMLInputElement).files[0];
  this.photoForm.patchValue({
    file: file,
  });
  this.photoForm.get('file').updateValueAndValidity();
  
  }

upFile( id : number){

  console.log(id);
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
jQuery('#photoModall').modal('hide');
      this.clearFormGroup(this.photoForm);
      this.getRotaGaleriById(this.rota.rotaId);
      console.log(data);
      this.alertifyService.success(data);
})
}
// GÜNCELLEME 


// consoleLog(rotaGaleriId: number) {
//   console.log(rotaGaleriId);
// }



openModal(rotaGaleri: RotaGaleri) {
  this.updateForm.patchValue({
    rotaGaleriId: rotaGaleri.rotaGaleriId,
    rotaId: rotaGaleri.rotaId,
    foto: rotaGaleri.foto,
    baslik: rotaGaleri.baslik,
    aciklama: rotaGaleri.aciklama,
    yayin: rotaGaleri.yayin
  });
  jQuery('#rotagaleriedit').modal('show');
}


updateRotaGaleri(): void {
  const rotaGaleriId = this.updateForm.get('rotaGaleriId').value;

  const updatedRotaGaleri: RotaGaleri = {
    rotaGaleriId: rotaGaleriId,
    rotaId: this.updateForm.get('rotaId').value,
    foto: this.updateForm.get('foto').value,
    baslik: this.updateForm.get('baslik').value,
    aciklama: this.updateForm.get('aciklama').value,
    yayin: this.updateForm.get('yayin').value,
    resimTipiId: this.updateForm.get('resimTipiId').value
  };
  const index = this.rotaGaleriList.findIndex(x => x.rotaGaleriId == rotaGaleriId);
  this.rotaGaleriList[index] = updatedRotaGaleri;
  this.rotaGaleri = new RotaGaleri();
  jQuery('#rotagaleriedit').modal('hide');

  this.clearFormGroup(this.rotaGaleriAddForm);

  this.rotaGaleriService.updateRotaGaleri(updatedRotaGaleri).subscribe(
    response => {
      // Güncelleme işlemi başarılı
      console.log('Güncelleme işlemi başarılı:', response);

      // Güncellemeden sonra verileri yenilemek için
      this.getRotaGaleriById(this.rota.rotaId);
    },
    error => {
      // Güncelleme işlemi hatası
      console.error('Güncelleme işlemi hatası:', error);
    }
  );
}

}