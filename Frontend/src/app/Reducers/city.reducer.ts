import { Action, createReducer, on } from '@ngrx/store';
import { saveCity } from '../Actions/city.actions';
import { CityModel } from '../Models/CityModel';
 
export const initialState = new CityModel();
 
const _cityReducer = createReducer<CityModel>(
  initialState,
  on(saveCity, (state, { city }) => ({id : city.id, name : city.name}))
);
 
export function cityReducer(
    state : CityModel | undefined, 
    action : Action) {

    return _cityReducer(state, action);
}