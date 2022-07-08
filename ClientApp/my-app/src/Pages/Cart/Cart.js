import React, { useState, useEffect } from "react";
import Axios from 'axios';
import '../../Componet/commen.css';
import { API_URLS } from "../../Componet/API/allAPI";
const { RoleMast } = API_URLS;

function Cart() {

    const [user, setuser] = useState([]);

    const fetchCartList = async () => {
        let obj = {};

        const { data } = await Axios.post(
            RoleMast.Select, obj
        );
        if (data.data != null) {
            const list = data.data.list;
            setuser(list);
        }
        else {
            setuser([]);
        }
    }

    useEffect(() => {
        fetchCartList();
    }, []);

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <h5 className='text-start'>Cart</h5>
                    <div className='col-lg-2 col-12 col-md-6'>
                        <select className='form-control'>
                            <option value="0">Select Role</option>
                            {user.map(user =>
                                <option key={user.id} value={user.id}>{user.name}</option>
                            )}
                        </select>
                    </div>
                </div>
            </div>

        </>
    )
}

export default Cart;