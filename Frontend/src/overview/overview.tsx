//from Ch3Unit1

import { PersonEntry } from "../app"
import { JSX } from "react"
import React from "react"
import { Map } from "immutable"

export const Overview = (props: {
    people: Map<number, PersonEntry>
    backToHome: () =>  void
  }): JSX.Element => (
    <div>
    {
      props.people.valueSeq().toArray().map(
        person => (
          <div key={`overview-list-item-${person.id}`}>
            Name: {person.name} Last Name: {person.lastName} Age: {person.age}
          </div>
        )
      )
    }
    <div>
      <button
        onClick={_ => props.backToHome()}
      >
        Back
      </button>
    </div>
    </div>
  )