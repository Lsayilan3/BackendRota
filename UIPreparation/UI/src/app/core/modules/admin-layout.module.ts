import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslationService } from 'app/core/services/translation.service';
import { LanguageComponent } from '../components/admin/language/language.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../components/admin/log/logDto.component';
import { MatSortModule } from '@angular/material/sort';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { BolgelerComponent } from 'app/bolgeler/bolgeler.component';
import { KategoriComponent } from 'app/kategori/kategori.component';
import { RotaComponent } from 'app/rota/rota.component';
import { RotaDetayiComponent } from 'app/rotaDetayi/rotaDetayi.component';
import { RotaGaleriComponent } from 'app/rotaGaleri/rotaGaleri.component';
import { SehirComponent } from 'app/sehir/sehir.component';
import { UlkeComponent } from 'app/ulke/ulke.component';
import { YorumlarComponent } from 'app/yorumlar/yorumlar.component';
import { RotaPagesComponent } from 'app/rotaPages/rotaPages.component';
import { ResimTipiComponent } from 'app/resimTipi/resimTipi.component';
import { GunlerComponent } from 'app/gunler/gunler.component';
import { PuanComponent } from 'app/puan/puan.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { RotaAnasayifaComponent } from 'app/rotaAnasayifa/rotaAnasayifa.component';
import { TeamComponent } from 'app/team/team.component';
import { SliderComponent } from 'app/slider/slider.component';
import { PartnerComponent } from 'app/partner/partner.component';
import { DestekComponent } from 'app/destek/destek.component';
// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        AngularEditorModule,
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],

    declarations: [
        DashboardComponent,
        UserComponent,
        LoginComponent,
        GroupComponent,
        LanguageComponent,
        TranslateComponent,
        OperationClaimComponent,
        LogDtoComponent,
        BolgelerComponent,
        KategoriComponent,
        RotaComponent,
        RotaDetayiComponent,
        RotaGaleriComponent,
        SehirComponent,
        UlkeComponent,
        YorumlarComponent,
        RotaPagesComponent,
        ResimTipiComponent,
        GunlerComponent,
        PuanComponent,
        RotaAnasayifaComponent,
        TeamComponent,
        SliderComponent,
        PartnerComponent,
        DestekComponent,


    ]
})

export class AdminLayoutModule { }
