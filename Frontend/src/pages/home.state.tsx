//From Ch3Unit1

import {Person} from "../app.state"
import { fromJS, Map } from "immutable"
import { RegistrationState, initRegistrationState} from "./registration.state"

//export type PageType = "homepage" | "products" | "product"  //replace with ViewState?
export type Route = { kind:"homepage" } | { kind:"registration" } | { kind:"overview" }
    
export type ViewState = 
  "home" | 
  "registration" |
  "overview"

export type PersonEntry = Person & {id: number}


export interface HomeState {
  storage: Map<number, PersonEntry>
  currentId: number
  view: ViewState
  registrationState: RegistrationState // added in 'lifting'
  updateRegistrationState: (updater: (registrationState: RegistrationState) => RegistrationState) => (state: HomeState) => HomeState, // added in lifting
  setViewState: (view: ViewState) => (state: HomeState) => HomeState
  setStorage: (storage: Map<number, Person & { id: number }>) => (state: HomeState) => HomeState
  insertPerson: (person: Person) => (state: HomeState) => HomeState
}

export const initHomeState: HomeState = {
  storage: Map(),
  currentId: 0,
  view: "home",
  registrationState: initRegistrationState,
  setViewState: (view: ViewState) => (state: HomeState): HomeState => ({
    ...state,
    view: view
  }),
  setStorage: (storage: Map<number, Person & { id: number }>) => (state: HomeState): HomeState => ({
    ...state,
    storage: storage
  }),
  insertPerson: (person: Person) => (state: HomeState): HomeState => ({
    ...state,
    currentId: state.currentId + 1,
    storage: state.storage.set(state.currentId, {
      ...person,
      id: state.currentId
    })
  }),
  updateRegistrationState: (updater: (registrationState: RegistrationState) => RegistrationState) => (state: HomeState): HomeState => ({
    ...state,
    registrationState: updater(state.registrationState)
  }),
}


    