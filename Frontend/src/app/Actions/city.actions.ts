import { createAction, props } from '@ngrx/store';
import { CityModel } from '../Models/CityModel';

export const saveCity = createAction('[Header Component] saveCity', props<{city: CityModel}>());