import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';

import { KategoriComponent } from 'app/kategori/kategori.component';
import { RotaComponent } from 'app/rota/rota.component';
import { RotaDetayiComponent } from 'app/rotaDetayi/rotaDetayi.component';
import { RotaGaleriComponent } from 'app/rotaGaleri/rotaGaleri.component';
import { SehirComponent } from 'app/sehir/sehir.component';
import { UlkeComponent } from 'app/ulke/ulke.component';
import { YorumlarComponent } from 'app/yorumlar/yorumlar.component';
import { BolgelerComponent } from './../../../../../bolgeler/bolgeler.component';
import { RotaPagesComponent } from 'app/rotaPages/rotaPages.component';
import { ResimTipiComponent } from 'app/resimTipi/resimTipi.component';
import { GunlerComponent } from 'app/gunler/gunler.component';
import { PuanComponent } from 'app/puan/puan.component';
import { RotaAnasayifaComponent } from 'app/rotaAnasayifa/rotaAnasayifa.component';
import { DestekComponent } from 'app/destek/destek.component';
import { PartnerComponent } from 'app/partner/partner.component';
import { SliderComponent } from 'app/slider/slider.component';
import { TeamComponent } from 'app/team/team.component';



export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},

    
    { path: 'bolgeler',            component: BolgelerComponent,canActivate:[LoginGuard]},
    { path: 'kategori',            component: KategoriComponent,canActivate:[LoginGuard]},
    { path: 'rota',            component: RotaComponent,canActivate:[LoginGuard]},
    { path: 'rotaDetayi',            component: RotaDetayiComponent,canActivate:[LoginGuard]},
    { path: 'rotaGaleri',            component: RotaGaleriComponent,canActivate:[LoginGuard]},
    { path: 'sehir',            component: SehirComponent,canActivate:[LoginGuard]},
    { path: 'ulke',            component: UlkeComponent,canActivate:[LoginGuard]},
    { path: 'yorumlar',            component: YorumlarComponent,canActivate:[LoginGuard]},
    { path: 'resimTipi',            component: ResimTipiComponent,canActivate:[LoginGuard]},
    { path: 'rotapages',            component: RotaPagesComponent,canActivate:[LoginGuard]},
    { path: 'rotapages/:rotaId', component: RotaPagesComponent ,canActivate:[LoginGuard] },
    { path: 'günler', component: GunlerComponent ,canActivate:[LoginGuard] },
    { path: 'puanlar', component: PuanComponent ,canActivate:[LoginGuard] },
    { path: 'rotaOnUc', component: RotaAnasayifaComponent ,canActivate:[LoginGuard] },
    { path: 'destek', component: DestekComponent ,canActivate:[LoginGuard] },
    { path: 'partner', component: PartnerComponent ,canActivate:[LoginGuard] },
    { path: 'slider', component: SliderComponent ,canActivate:[LoginGuard] },
    { path: 'team', component: TeamComponent ,canActivate:[LoginGuard] },
    
];
