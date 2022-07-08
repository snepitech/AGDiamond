import '../Componet/commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from "react";
import Axios from 'axios';
import Save from '../Componet/Button/BtnSave';
import DataTable from '../Componet/Data-Table/DataTable';
import { API_URLS } from "../Componet/API/allAPI";
const { PageList } = API_URLS;

export default function Pagelist() {

    const [pagelist, setPageList] = useState([]);

    const fetchPageList = async () => {
        let obj = {};
        const { data } = await Axios.post(
            PageList.Select, obj
        );
        if (data.data != null) {
            const list = data.data.list;
            setPageList(list);
        }
    }

    useEffect(() => {
        fetchPageList();
    }, []);

    const colums = [
        {
            name: 'No',
            selector: 'id',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Page Name',
            selector: 'page_name',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Page Name Sub',
            selector: 'page_name_sub',
            width: 'auto',
        },
        {
            name: 'Page Name Sub',
            selector: 'page_name_sub_more',
            width: 'auto',
        },
        {
            name: 'Is Active',
            selector: 'is_active',
            width: '50px',
        }
    ]

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-lg-1 col-6 col-md-6 col-xs-12'>
                        <h5 className='text-start'>Page List</h5>
                    </div>
                    <div className='col-lg-3 col-6 col-md-6 col-xs-12'>
                        <Save label="New" primaryBtn />
                        <Save label="Refresh" primaryBtn />
                        <Save label="Add" primaryBtn />
                        <Save label="Save" primaryBtn />
                        <Save label="Save Button" primaryBtn />
                    </div>
                </div>
                <div className='row align-items-start justify-content-start'>
                    <div className='col-xl-4 col-lg-6 col-12 col-md-9 col-sm-12 border'>
                        <DataTable columns={colums} data={pagelist} />
                    </div>
                    <div className='col-xl-4 col-lg-6 col-12 col-md-9 col-sm-12 border'>
                        <DataTable columns={colums} data={pagelist} />
                    </div>
                </div>
            </div>
        </>
    )
} 