//From Ch3Unit1

import React from "react"
import { Map } from "immutable"
import { Person, AppState, initAppState } from "./app.state"


export type ViewState = 
  "home" | 
  "registration" |
  "overview"

export type PersonEntry = Person & {id: number}


export class App extends React.Component<{}, AppState> {
  constructor(props: {}) {
    super(props)
    this.state = initAppState
  }

  render(){
    return (
      <div>
        <div key={`registration-form-name`}>
          First Name:
          <input
            value={this.state.name}
            onChange={e => this.setState(this.state.updateName(e.currentTarget.value))}
          />
        </div>
        <div key={`registration-form-last-name`}>
          Last Name:
          <input
            value={this.state.lastName}
            onChange={e => this.setState(this.state.updateLastName(e.currentTarget.value))}
          />
        </div>
        <div key={`registration-form-age`}>
          Age:
          <input
            value={this.state.age}
            type={"number"}
            onChange={e => this.setState(this.state.updateAge(e.currentTarget.valueAsNumber))}
          />
        </div>
        <div>
          <button
            onClick={_ => this.setState(this.state.insertPerson)}
          >
              Submit
          </button>
        </div>
        <div>
          {
            this.state.storage.valueSeq().toArray().map(
              person => (
                <div key={`overview-list-item-${person.id}`}>
                  Name: {person.name} Last Name: {person.lastName} Age: {person.age}
                </div>
              )
            )
          }
        </div>
      </div>
    )
  }
}

