import '../../Componet/commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Axios from 'axios';
import React, { useState, useEffect } from "react";
import DataTable from '../../Componet/Data-Table/DataTable';
import { API_URLS } from "../../Componet/API/allAPI";
const { RoleMast, PageList } = API_URLS;

export default function Role() {

    const [pagelist, setPageList] = useState([]);

    const fetchPageList = async () => {
        let obj = {};
        const { data } = await Axios.post(
            RoleMast.Select, obj
        );
        if (data.data != null) {
            const list = data.data.list;
            setPageList(list);

            // alert((list[0]['id']));
        }
    }

    useEffect(() => {
        fetchPageList();
    }, []);

    const colums = [
        {
            name: 'ID',
            selector: 'id',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Name',
            selector: 'name',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Code',
            selector: 'code',
            width: 'auto',
        },
        {
            name: 'is active',
            selector: 'is_active',
            width: 'auto',
        },
        {
            name: '',
            cell: (d) => <><input type="checkbox" className="checkBox" /></>,
            width: '50px',
        },
    ]

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <h5 className='text-start'>Role</h5>
                </div>
            </div>
            <div className='mt-1 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-xl-12 col-12'>
                        <DataTable
                            columns={colums}
                            data={pagelist}
                        />
                    </div>
                </div>
            </div>

        </>
    )
}