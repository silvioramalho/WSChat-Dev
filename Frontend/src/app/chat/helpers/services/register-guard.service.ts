import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  CanActivate,
} from '@angular/router';
import { Observable } from 'rxjs';
import { LevelService } from './level.service';

@Injectable({
  providedIn: 'root',
})
export class RegisterGuardService implements CanActivate {
  constructor(private levelService: LevelService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    if (this.levelService.isRegistrered()) {
      this.router.navigate(['chat', 'rooms']);
    } else {
      return true;
    }
  }
}
