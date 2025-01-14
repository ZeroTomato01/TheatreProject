import React from 'react';

const Admin: React.FC = () => {
    return (
        <div>
            <h1>Admin Page</h1>
            <ul>
                <li>
                    <button>
                        Add Show
                    </button>
                </li>
                <li>
                    <button>
                        Delete Show
                    </button>
                </li>
                <li>
                    <button>
                        Delete Customer
                    </button>
                </li>
            </ul>
        </div>
    );
};

export default Admin;