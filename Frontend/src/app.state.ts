//from Ch3Unit1

import { Map } from "immutable"

export interface Person {
    name: string
    lastName: string
    age: number
  }
  
export type AppState = Person & {
storage: Map<number, Person & {id: number}>
currentId: number
updateName: (name: string) => (state: AppState) => AppState
updateLastName: (lastName: string) => (state: AppState) => AppState
updateAge: (age: number) => (state: AppState) => AppState,
insertPerson: (state: AppState) => AppState
  }

export const initAppState: AppState = {
name: "",
lastName: "",
age: 18,
storage: Map(),
currentId: 0,
updateName: (name: string) => (state: AppState): AppState => ({
    ...state,
    name: name
}),
updateLastName: (lastName: string) => (state: AppState): AppState => ({
    ...state,
    lastName: lastName
}),
updateAge: (age: number) => (state: AppState): AppState => ({
    ...state,
    age: age
}),
insertPerson: (state: AppState): AppState => ({
    ...state,
    currentId: state.currentId + 1,
    storage: state.storage.set(state.currentId, {
    id: state.currentId,
    name: state.name,
    lastName: state.lastName,
    age: state.age
    })
})
}