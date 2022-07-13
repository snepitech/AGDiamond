import '../../Componet/commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Axios from 'axios';
import React, { useState, useEffect } from "react";
import Save from '../../Componet/Button/BtnSave';
import Reset from '../../Componet/Button/BtnReset';
import CheckBox from '../../Componet/CheckBox/Check';
import DataTable from '../../Componet/Data-Table/DataTable';
import editImg from '../../images/edit.png';
import removeImg from '../../images/image.png';
import { API_URLS } from "../../Componet/API/allAPI";
const { RoleMast } = API_URLS;

async function addRole(credentials) {
    return fetch(RoleMast.Insert, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}
async function deleteRole(credentials) {
    return fetch(RoleMast.Delete, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}

export default function Role() {

    const [pagelist, setPageList] = useState([]);
    const [modal, setmodal] = useState(false);
    const [name, setName] = useState();
    const [code, setCode] = useState();

    const fetchRoleList = async () => {
        let obj = {};
        const { data } = await Axios.post(
            RoleMast.Select, obj
        );
        if (data.data != null) {
            const list = data.data.list;
            setPageList(list);
        }
    }

    useEffect(() => {
        fetchRoleList();
    }, []);

    const refreshFn = () => {
        fetchRoleList();
        setmodal(false);
        setCode("");
        setName("");
    }

    const deleteFn = async (row) => {
        if (row.id) {
            const res = await deleteRole({ id: row.id });
            if (res.flag) {
                fetchRoleList();
            }
            else {
                alert(res.message);
            }
        }
    }

    const editFn = (row) => {
        setmodal(true);
        setCode(row.code);
        setName(row.name);
    } 

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
        // {
        //     name: 'Is Active',
        //     selector: 'is_active',
        //     cell: (d) => <><input type="checkbox" checked={d.is_active} className="checkBox" /></>,
        //     width: 'auto',
        // },
        {
            name: 'Is Active',
            selector: 'is_active',
            cell: (d) => <><CheckBox checked={d.is_active} /></>,
            width: 'auto',
        },

        {
            name: 'Action',
            Button: true,
            cell: (d) => <><img src={editImg} width={13} onClick={() => editFn(d)} /><img src={removeImg} width={13} onClick={() => deleteFn(d)} className='ms-2' /></>,
        },
    ]

    const addFn = () => {
        setmodal(true);
    }

    const resetFn = () => {
        setCode("");
        setName("");
    }

    const saveFn = async () => {
        let CODE = code; 
        let NAME = name; 
        const res = await addRole({ CODE, NAME });
        if (res) {
            if (res.flag) {
                setmodal(false);
                refreshFn();
            }
            else {
                alert(res.message);
            }
        }
        fetchRoleList();
    }

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-lg-1 col-6 col-md-6 col-xs-12'>
                        <h5 className='text-start'>Role Master</h5>
                    </div>
                    <div className='col-lg-4 col-6 col-md-6 col-xs-12 d-flex text-end align-items-center justify-content-end'>
                            <Save label="Refresh" primaryBtn onClick={refreshFn} />
                            <Save label="Add" primaryBtn onClick={addFn} />
                        {modal && (
                            <div>
                                <Save label="Save" primaryBtn onClick={saveFn} />
                                {/* <Save label="Save & Continue" primaryBtn /> */}
                                <Reset label="Reset" primaryBtn onClick={resetFn} />
                            </div>
                        )}
                    </div>
                </div>
            </div>
            {modal && (
                <div className='row align-items-center justify-content-between mb-3'>
                    <div className='col-12 d-flex'>
                        <div className='ms-3'>Code : <input type="text" value={code} placeholder="code" className='ps-1' onChange={e => setCode(e.target.value)} /></div>
                        <div className='ms-3'>Name : <input type="text" value={name} placeholder="name" className='ps-1' onChange={e => setName(e.target.value)} /></div>
                    </div>
                </div>
            )}
            <div className='mt-1 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-xl-12 col-12 border'>
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