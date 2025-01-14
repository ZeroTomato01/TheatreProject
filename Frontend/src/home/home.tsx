//From Ch3Unit1

import React from 'react';
import { JSX } from "react";
import { Person } from '../app.state';
import { HomeState, initHomeState } from "./home.state"
import { RegistrationForm } from '../registration/registration';
import { Overview } from '../overview/overview';
import MenuBar from '../components/MenuBar';

export class Home extends React.Component<{}, HomeState> { //Home is a COMPONENT, just like RegistrationForm
    constructor(props: {}) {
        super(props)
        this.state = initHomeState
    }
    
    render(): JSX.Element {
        switch (this.state.view) {
          case "home":
            let isLoggedIn = false;

            return (
              <div>
                <MenuBar isLoggedIn={isLoggedIn}/>
                  <div>
                  Welcome to the person management system!
                  <div>
                    <button
                      onClick={_ => this.setState(this.state.setViewState("registration"))}
                    >
                      Registration
                    </button>
                  </div>
                  <div>
                    <button
                      onClick={_ => this.setState(this.state.setViewState("overview"))}
                    >
                      Overview
                    </button>
                  </div>
                </div>
              </div>
            )

            case "registration":
                return (
                  <RegistrationForm 
                    insertPerson={(person: Person) => this.setState(this.state.insertPerson(person))}
                    backToHome={() => this.setState(this.state.setViewState("home"))}
                    registrationState={this.state.registrationState} //added in lifting
                    updateName={(name: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateName(name)))} //added in lifting
                    updateLastName={(lastName: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateLastName(lastName)))} //added in lifting
                    updateAge={(age: number) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateAge(age)))} //added in lifting
                        //exercise 2
                    updateMessage={(message: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateMessage(message)))}
                  /> 
            )
            case "overview":
            return (
                <Overview
                people={this.state.storage}
                backToHome={() => this.setState(this.state.setViewState("home"))}
                />
            )

            default: return <></>
        }
      }
    }