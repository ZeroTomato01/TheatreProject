//from Ch3Unit1

import React from 'react';
import { JSX } from "react";
import { RegistrationState} from "./Registration.state"
import { Person } from "../app.state"



export interface RegistrationProps {
    insertPerson: (_: Person) => void
    backToHome: () => void
    registrationState: RegistrationState //added during 'lifting'
    updateName: (name: string) => void //added during 'lifting'
    updateLastName: (lastName: string) => void //added during 'lifting'
    updateAge: (age: number) => void //added during 'lifting'
    updateMessage: (message: string) => void //for exercise 2
  }
  
export class RegistrationForm extends React.Component<RegistrationProps, RegistrationState> { 
    constructor(props: RegistrationProps) {
      super(props)
      this.state = props.registrationState // was '= initRegistrationState' before lifting
    }

    render(){
        return (
            <div>
                <div key={'registration-success-message'}>
                    {this.state.message}
                </div>
                <div key={`registration-form-name`}>
                    First Name:
                    <input
                        
                        value={this.props.registrationState.name}
                        onChange={e => {this.props.updateName(e.currentTarget.value);
                                        this.props.updateMessage("")}
                        }
                    />
                </div>
                <div key={`registration-form-last-name`}>
                    Last Name:
                    <input
                        value={this.props.registrationState.lastName}
                        onChange={e => {this.props.updateLastName(e.currentTarget.value);
                                        this.props.updateMessage("")}
                        }
                    />
                </div>
                <div key={`registration-form-age`}>
                    Age:
                    <input
                        value={this.props.registrationState.age}                  
                        type={"number"}
                        onChange={e => {this.props.updateAge(e.currentTarget.valueAsNumber);
                                        this.props.updateMessage("")}
                        }
                    />
                </div>
                <div>
                    <button
                        onClick={() => { //added these braces to chain commands for exercise 1
                            this.props.insertPerson({
                            name: this.props.registrationState.name,
                            lastName: this.props.registrationState.lastName,
                            age: this.props.registrationState.age});
                                //exercise 1
                            this.props.updateName("");
                            this.props.updateLastName("");
                            this.props.updateAge(18);
                                //exercise 2
                            this.props.updateMessage("User succesfully registered!");
                        }
                        }           
                    >
                        Submit
                    </button>
                </div>
                <div>
                    <button
                        onClick={_ => {this.props.backToHome();
                                        this.props.updateMessage("");}
                        }
                    >
                        Back
                    </button>
                </div>
            </div>
        )
    }

}
