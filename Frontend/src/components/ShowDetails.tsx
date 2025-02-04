import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useLocation } from "react-router-dom";
import { TheatreShow, TheatreShowDate } from './Shows';


interface ShowDetailsProps {
    //CustomerEmail: string
    Show: TheatreShow,
    ShowDate: TheatreShowDate
}

const ShowDetails = () => {
    const location = useLocation();
    const show: TheatreShow = location.state?.show;
    const showDate: TheatreShowDate = location.state?.showDate; // Access the passed data
  
    return (
      <div>
        <h1>Show Details</h1>
        {show ? (
            <div>
                <p>Title: {show.title}</p>
                <p>Description: {show.description}</p>
                <p>Price: {show.price}</p>
                <p>Venue Id: {show.venueId}</p>
            </div>
        ) : (
          <p>No show data received.</p>
        )}
        {showDate ? (
          <p>Show Date: {showDate.dateAndTime}</p>
        ) : (
          <p>No showDate data received.</p>
        )}
      </div>
    );
  };

  export default ShowDetails