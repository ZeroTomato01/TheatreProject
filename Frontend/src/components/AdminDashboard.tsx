import React from 'react';
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { TheatreShow, TheatreShowDate } from './Shows';
import { useAdmin } from './AdminContext';
import { Reservation } from './Reserve';


const AdminDashboard: React.FC = () => {
    //navigate to login screen if user accessed dashboard without being logged in
    const { isLoggedIn } = useAdmin();
    const navigate = useNavigate();
  
    useEffect(() => {
      if (!isLoggedIn) {
        navigate("/Login");
      }
    }, [isLoggedIn, navigate]);

    const [reservations, setReservations] = useState<Reservation[]>([]);
    useEffect(() => {
        fetch("/Reservation")
            .then(response => response.json())
            .then(data => setReservations(data))
            .catch(error => console.error("Error fetching reservations:", error));
    }, []); // Add empty dependency array

    //load in Shows and ShowDates
    const [shows, setShows] = useState<TheatreShow[]>([]);
    const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
    // useEffect(() => {
    // // Fetch TheatreShows
    // fetch("/TheatreShow")
    //     .then(response => response.json())
    //     .then(data => setShows(data))
    //     .catch(error => console.error("Error fetching shows:", error));

    // // Fetch TheatreShowDates separately
    // fetch("/TheatreShowDate/future")
    //     .then(response => response.json())
    //     .then(data => setShowDates(data))
    //     .catch(error => console.error("Error fetching show dates:", error));
    // }, []);

        // Function to fetch and update the shows list
        const fetchShows = async () => {
            try {
                const response = await fetch("/TheatreShow");
                if (!response.ok) {
                    throw new Error(`Failed to fetch shows: ${response.statusText}`);
                }
                setShows(await response.json()); // Update state with fetched shows
            } catch (error) {
                console.error("Error fetching shows:", error);
            }
        };

        const fetchShowDates = async () => {
            try {
                const response = await fetch("/TheatreShowDate/future");
                if (!response.ok) {
                    throw new Error(`Failed to fetch ShowDates: ${response.statusText}`);
                }
                setShowDates(await response.json()); // Update state with fetched shows
                console.log(showDates)
            } catch (error) {
                console.error("Error fetching shows:", error);
            }
        };

        // Fetch shows when the component mounts
        useEffect(() => {
            fetchShows();
            fetchShowDates();
        }, []);
    

    //Adding and Editing Shows
    const [showToAdd, setShowToAdd] = useState<TheatreShow>(
        {theatreShowId: 0,
        title: "",
        description: "",
        price: 0,
        theatreShowDateIds: [],
        venueId: 0,
        venue: undefined
        })

    const [isAddingShow, setIsAddingShow] = useState(false)
    const toggleIsAddingShow = () => {setIsAddingShow(!isAddingShow)}

    const [editedShow, setEditedShow] = useState<TheatreShow>({...showToAdd}) //makes shallow copy with default values
    const [isEditingShow, setIsEditingShow] = useState(false)
    const toggleIsEditingShow = (show: TheatreShow) => {
        setIsEditingShow(!isEditingShow);
        if (!isEditingShow) {
          setEditedShow({ ...show }); // Set the edited show only when entering edit mode
        }
      };

    //adding and editing ShowDates:
    const [showDateToAdd, setShowDateToAdd] = useState<TheatreShowDate>(
        { theatreShowDateId: 0,
            dateAndTime: "",
            theatreShowId: 0
        })
    const [isAddingShowDate, setIsAddingShowDate] = useState(false)
    const toggleIsAddingShowDate = async (show: TheatreShow) => {
        setIsAddingShowDate(!isAddingShowDate)
        if (!isAddingShowDate) {
            setShowDateToAdd({ ...showDateToAdd, theatreShowId: Number(show.theatreShowId)}); // Set the edited show only when entering edit mode
            const response = await fetch("/TheatreShowDate/count")
            if (response.ok){
                var count = await response.json()
                setShowDateToAdd({ ...showDateToAdd, theatreShowDateId: count + 1}); 
            }
            else{
                console.log("couldn't fetch count of ShowDates")
            }
            
        
        }
    }

    const [editedShowDate, setEditedShowDate] = useState<TheatreShowDate>({...showDateToAdd}) //makes shallow copy with default values
    const [isEditingShowDate, setIsEditingShowDate] = useState(false)
    const toggleIsEditingShowDate = (showDate: TheatreShowDate) => {
        setIsEditingShowDate(!isEditingShowDate);
        if (!isEditingShowDate) {
          setEditedShowDate({ ...showDate }); // Set the edited show only when entering edit mode
        }
      };


    const AddShow = async (e: React.FormEvent<HTMLFormElement>) => { 
        e.preventDefault();
        try {
            const response = await fetch("/TheatreShow", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(showToAdd),
            });

            if (!response.ok) {
                console.log("Error:", response.status, response.statusText)} 
            else {
                console.log("Succes");
                setIsAddingShow(false)
                fetchShows()
                fetchShowDates()}
        } 
        catch (error) {
            console.error("Fetch failed:", error + JSON.stringify(showDateToAdd));
        }
    };

    const EditShow = async () => {
        if(window.confirm("Are you sure you want te edit this show?")) {
            try {
                const response = await fetch("/TheatreShow", {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(editedShow),
                });
        
                if (!response.ok) {
                    console.log(`Error: ${response.status} : ${response.statusText}`);
                } else {
                    console.log("Success");
                    setIsEditingShow(false)
                    fetchShows()
                    fetchShowDates()
                }
            } catch (error) {
                console.error("Fetch failed:", error + JSON.stringify(showDateToAdd));
            }
        }
        else
            alert("canceled")
    }
    const DeleteShow = async (show: TheatreShow) => {
        if(window.confirm("Are you sure you want te delete this show?")) {
            try {
                const response = await fetch(`/TheatreShow?id=${show.theatreShowId}`, {
                    method: "DELETE",
                });
        
                if (!response.ok) {
                    throw new Error(`Failed to delete show: ${response.statusText}`);
                }
                else {
                    console.log("Show deleted successfully");
                    fetchShows()
                    fetchShowDates()
                    alert("show deleted")
                }
            } 
            catch (error) {
                console.error("Error deleting show:", error);
            }
        }
        else
            alert("canceled")
    };

    const AddShowDate = async (e: React.FormEvent<HTMLFormElement>) => { 
        e.preventDefault();
        try {
            const response = await fetch("/TheatreShowDate", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(showDateToAdd),
            });
            if (!response.ok) {
                console.log("Error:", response.status, response.statusText); 
            }
            else {
                console.log("Success");
                setIsAddingShowDate(false)
                fetchShows()
                fetchShowDates()}
        } 
        catch (error) {
            console.error("Fetch failed:", error + JSON.stringify(showDateToAdd));
        }
    };

    const EditShowDate = async () => {
        if(window.confirm("Are you sure you want te edit this show?")) {
            try {
                const response = await fetch("/TheatreShowDate", {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(editedShowDate),
                });
            if (!response.ok) {
                console.log("Error:", response.status, response.statusText);
            } else {
                console.log("Success");
                setIsEditingShowDate(false)
                fetchShows()
                fetchShowDates()
            }
        } catch (error) {
            console.error("Fetch failed:", error + JSON.stringify(showDateToAdd));
        }
        }
        else
            alert("canceled")
    }
    const DeleteShowDate = async (showDate: TheatreShowDate) => {
        if(window.confirm("Are you sure you want te delete this ShowDate?")) {
            try {
                const response = await fetch(`/TheatreShowDate?id=${showDate.theatreShowDateId}`, {
                    method: "DELETE",
                });
        
                if (!response.ok) {
                    throw new Error(`Failed to delete ShowDate: ${response.statusText}`);
                }
                else {
                    console.log("ShowDate deleted successfully");
                    fetchShows()
                    fetchShowDates()
                    alert("ShowDate deleted")
                }
                
            } catch (error) {
                console.error("Error deleting ShowDate:", error);
            }
        }
        else
            alert("canceled")
        
    };
    

    return (
        
        <div>
            <h1>Admin Dashboard</h1>
            <button onClick={e => console.log(showDates)}> debug button</button>
            <p>Shows</p>
            <button onClick={e => toggleIsAddingShow()}> {isAddingShow == false ? "Add Show" : "Cancel"} </button>
            {isAddingShow == true 
                ? <form onSubmit={AddShow}>
                    <div>
                        Title: <input 
                        value={showToAdd.title}
                        onChange={e => setShowToAdd({...showToAdd, title: e.target.value})}
                        />
                    </div>
                    <div>
                        Id: 
                        <input 
                            type="number"
                            value={showToAdd.theatreShowId === 0 ? "" : showToAdd.theatreShowId} // Show empty when it's 0
                            onChange={e => {
                                const value = e.target.value;
                                setShowToAdd({
                                    ...showToAdd,
                                    theatreShowId: value === "" ? 0 : Number(value) // Use 0 as empty value
                                });
                            }}
                        />
                    </div>
                    <div>
                        Description: <input 
                        value={showToAdd.description}
                        onChange={e => setShowToAdd({...showToAdd, description: e.target.value})}
                        />
                    </div>
                    <div>
                        Price: 
                        <input 
                            type="number"
                            value={showToAdd.price === 0 ? "" : showToAdd.price} // Show empty when it's 0
                            onChange={e => setShowToAdd({...showToAdd, price: e.target.valueAsNumber})}/>
                    </div>
                    <div>
                        VenueId: 
                        <input 
                            type="number"
                            value={showToAdd.venueId === 0 ? "" : showToAdd.venueId} // Show empty when it's 0
                            onChange={e => setShowToAdd({...showToAdd, venueId: e.target.valueAsNumber})}/>
                    </div>
                    <button> Submit </button>

                </form> 
                : ""}
            <ul>
            {shows.length > 0 ? (
                shows.map(show => (
                <li key={show.theatreShowId}>
                    <strong>{show.title}     </strong> 
                    {/* <button onClick={e => EditShow}> Edit Show </button> */}
                    
                    <button onClick={e => toggleIsEditingShow(show)}> {isEditingShow == true && editedShow.theatreShowId == show.theatreShowId ? "Cancel" : "Edit ShowDate"} </button>
                        {isEditingShow == true && editedShow.theatreShowId == show.theatreShowId ? 
                        
                        <form onLoad={e=> setEditedShow({...show})}
                        onSubmit={e=> {e.preventDefault(); EditShow();}}>
                            <div>
                                Title:
                                <input
                                    value ={editedShow.title}
                                    onChange={e => setEditedShow({...editedShow, title: e.target.value})} />
                            </div>
                            <div>
                                Description:
                                <input
                                    value ={editedShow.description}
                                    onChange={e => setEditedShow({...editedShow, description: e.target.value})} />
                            </div>
                            <div>
                                Price: 
                                <input 
                                    type="number"
                                    value={editedShow.price === 0 ? "" : editedShow.price} // Show empty when it's 0
                                    onChange={e => setEditedShow({...editedShow, price: e.target.valueAsNumber})}/>
                            </div>
                            <div>
                                VenueId: 
                                <input 
                                    type="number"
                                    value={editedShow.venueId === 0 ? "" : editedShow.venueId} // Show empty when it's 0
                                    onChange={e => setEditedShow({...editedShow, venueId: e.target.valueAsNumber})}/>
                            </div>
                            <button> Submit </button>
                        </form>
                        : ""
                        
                    }
                    <button onClick={e => DeleteShow(show)}> Delete Show </button>
                    <p>{show.description}</p>
                    <p><strong>Price:</strong> ${show.price}</p>
                    <p><strong>Venue:</strong> {show.venue?.name}</p>
                  
                    <button onClick={e => toggleIsAddingShowDate(show)}> {isAddingShowDate == true && showDateToAdd.theatreShowId == show.theatreShowId ? "Cancel" : "Add ShowDate"} </button>
                    {isAddingShowDate == true && showDateToAdd.theatreShowId == show.theatreShowId
                        ?<form onSubmit={AddShowDate}>
                            {/* <div>
                            TheatreShowDateId: 
                                <input 
                                    type="number"
                                    value={showDateToAdd.theatreShowDateId === 0 ? "" : showDateToAdd.theatreShowDateId} // Show empty when it's 0
                                    onChange={e => setShowDateToAdd({...showDateToAdd, theatreShowDateId: e.target.valueAsNumber})}/>
                            </div> */}
                            <div>
                                Date and Time: <input 
                                value={showDateToAdd.dateAndTime}
                                onChange={e => setShowDateToAdd({...showDateToAdd, dateAndTime: e.target.value})}
                                />
                            </div>
                            
                            {/* <div>
                            TheatreShowId: 
                                <input 
                                    type="number"
                                    value={showDateToAdd.theatreShowId === 0 ? "" : showDateToAdd.theatreShowId} // Show empty when it's 0
                                    onChange={e => setShowDateToAdd({...showDateToAdd, theatreShowId: e.target.valueAsNumber})}/>
                            </div> */}
                            <button> Submit </button>
                        </form> 
                        : ""}

                    <ul>
                    {showDates
                        .filter(date => date.theatreShowId === show.theatreShowId) // Match ShowDates with Show ID
                        .map((showDate) => (
                        <li key={showDate.theatreShowDateId}>
                            <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString() + "     "}   
                            <button onClick={e => toggleIsEditingShowDate(showDate)}> 
                            {isEditingShowDate == true 
                            && editedShowDate.theatreShowId == showDate.theatreShowId
                            && editedShowDate.theatreShowDateId == showDate.theatreShowDateId
                              ? "Cancel" : "Edit ShowDate"} </button>
                            {isEditingShowDate == true 
                            && editedShowDate.theatreShowId == showDate.theatreShowId
                            &&  editedShowDate.theatreShowDateId == showDate.theatreShowDateId
                                ?
                                <form 
                                // onLoad={e=> setEditedShowDate({...showDate})}
                                onSubmit={e=> {e.preventDefault(); EditShowDate();}}>
                                    <div>
                                        DateAndTime:
                                        <input
                                            value ={editedShowDate.dateAndTime}
                                            onChange={e => setEditedShowDate({...editedShowDate, dateAndTime: e.target.value})} />
                                    </div>
                                    {/* <div>
                                        TheatreShowId: 
                                        <input 
                                            type="number"
                                            value={editedShowDate.theatreShowId === 0 ? "" : editedShowDate.theatreShowId} // Show empty when it's 0
                                            onChange={e => setEditedShowDate({...editedShowDate, theatreShowId: e.target.valueAsNumber})}/>
                                    </div> */}
                                    <button> Submit </button>

                                </form> 
                            : ""}
                            <button onClick={e => DeleteShowDate(showDate)}> Delete ShowDate</button>

                        </li>
                        ))}
                    </ul>
                </li>
                ))
            ) : (
                <li>Loading shows...</li>
            )}
            </ul>
            <div>
                <h1>Reservations</h1>
                {reservations.map((reservation) => (
                    <div key={reservation.reservationId}>
                        <p>Reservation ID: {reservation.reservationId}</p>
                        <p>Tickets: {reservation.amountOfTickets}</p>
                        <p>Status: {reservation.used ? "Used" : "Not Used"}</p>
                        {reservation.theatreShowDateId && (
                            <p>Show Date ID: {reservation.theatreShowDateId}</p>
                        )}
                        <hr /> {/* Add a horizontal line between reservations */}
                    </div>
                ))}
            </div>


        </div>
    );
};

export default AdminDashboard;