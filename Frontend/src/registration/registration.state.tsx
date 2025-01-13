//from Ch3Unit1


import { Person } from "../app.state"
import React from 'react';

export type RegistrationState = Person & {
    updateName: (name: string) => (state: RegistrationState) => RegistrationState
    updateLastName: (lastName: string) => (state: RegistrationState) => RegistrationState
    updateAge: (age: number) => (state: RegistrationState) => RegistrationState
    message: string//for exercise 2
    updateMessage: (message: string) => (state: RegistrationState) => RegistrationState
  }
  
export const initRegistrationState: RegistrationState = { //is still used in home.state after lifting
    name: "",
    lastName: "",
    age: 18,
    message: "",
    updateName: (name: string) => (state: RegistrationState): RegistrationState => ({
      ...state,
      name: name
    }),
    updateLastName: (lastName: string) => (state: RegistrationState): RegistrationState => ({
      ...state,
      lastName: lastName
    }),
    updateAge: (age: number) => (state: RegistrationState): RegistrationState => ({
      ...state,
      age: age
    }),
    updateMessage: (message: string) => (state: RegistrationState): RegistrationState => ({
        ...state,
        message: message
    })
  }

 