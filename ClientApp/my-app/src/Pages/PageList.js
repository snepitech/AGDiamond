import '../Componet/commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useState, useEffect } from "react";
import Axios from 'axios';
import Save from '../Componet/Button/BtnSave';
import DataTable from '../Componet/Data-Table/DataTable';
import editImg from '../images/edit.png';
import removeImg from '../images/remove.png';
import Add from '../images/Add.png';
import { API_URLS } from "../Componet/API/allAPI";
const { PageList, PageControls } = API_URLS;

async function addMaster(credentials) {
    return fetch(PageList.Insert, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}
async function updateMaster(credentials) {
    return fetch(PageList.Update, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}
async function deleteMaster(credentials) {
    return fetch(PageList.Delete, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}
async function addBtn(credentials) {
    
    return fetch(PageControls.Insert, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}
async function deleteBtn(credentials) {
    return fetch(PageControls.Delete, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            "Accept": "application/json"
        },
        body: JSON.stringify(credentials)
    })
        .then(data => data.json());
}

export default function Pagelist() {

    const [pagelist, setPageList] = useState([]);
    const [btnlist, setbtnList] = useState([]);
    const [modal, setmodal] = useState(false);
    const [Btnmodal, setBtnmodal] = useState(false);
    const [isActive, setIsActive] = useState(true);

    const [id, setID] = useState();
    const [name, setName] = useState([]);
    const [namesub1, setNamesub1] = useState();
    const [namesub2, setNamesub2] = useState();
    const [btnid, setbtnid] = useState();
    const [btnmessage, setMessage] = useState('');

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

    const fatchbtnList = async (row) => {
        let obj = {};
        const { data } = await Axios.post(
            PageControls.Select, { id }
        );
        if (data.data != null) {
            const list = data.data.list;
            setbtnList(list);
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
            name: 'Name',
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
            cell: (d) => <><input type="checkbox" checked={d.is_active} className="checkBox" /></>,
            width: '100px',
        },
        {
            name: 'Action',
            Button: true,
            cell: (d) => <><img src={editImg} width={13} onClick={() => editFn(d)} /><img src={removeImg} width={13} onClick={() => deleteFn(d)} className='ms-2' /></>,
        },
    ]

    const btncolums = [
        {
            name: 'No',
            selector: 'id',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Button Name',
            selector: 'button_name',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Button Color',
            selector: 'button_color',
            sortable: true,
            width: 'auto',
        },
        {
            name: 'Action',
            Button: true,
            cell: (d) => <><img src={removeImg} width={13} onClick={() => deleteFn1(d)} className='ms-2' /></>,
            width: '50px',
        },
    ]

    const refreshFn = () => {
        fetchPageList();
        setmodal(false);
        setName("");
        setNamesub1("");
        setNamesub2("");
    }

    const editFn = async (row) => {
        setID(row.id);
        setName(row.page_name);
        setNamesub1(row.page_name_sub);
        setNamesub2(row.page_name_sub_more);
        setIsActive(row.is_active);
        setmodal(true);
        fatchbtnList();
    }

    const updateFn = async () => {
        let is_active = (isActive) ? 1 : 0;
        let page_name = name;
        let page_name_sub = namesub1;
        let page_name_sub_more = namesub2;
        alert(isActive);
        const res = await updateMaster({
            id,
            page_name, page_name_sub, page_name_sub_more, is_active
        });
        if (res) {
            if (res.flag) {
                fetchPageList();
                setmodal(false);
            }
            else {
                alert(res.message);
            }
        }
    }

    const deleteFn = async (row) => {
        if (row.id) {
            const res = await deleteMaster({ id: row.id });
            if (res.flag) {
                fetchPageList();
            }
            else {
                alert(res.message);
            }
        }
    }

    const deleteFn1 = async (row) => {
        if (row.id) {
            const res = await deleteBtn({ id: row.id });
            alert(row.id);
            if (res.flag) {
                fatchbtnList();
            }
            else {
                alert(res.message);
            }
        }
    }

    const addFn = () => { setmodal(true); }

    const saveFn = async e => {
        let is_active = (isActive) ? 1 : 0;
        let page_name = name;
        let page_name_sub = namesub1;
        let page_name_sub_more = namesub2;
        const res = await addMaster({
            id, 
            page_name, page_name_sub, page_name_sub_more, is_active
        });
        if (res) {
            if (res.flag) {
                if (e) {
                    setmodal(true);
                }
                else {
                    setmodal(false);
                }
                refreshFn();
                fetchPageList();
            }
            else {
                alert(res.message);
            }
        }
    }

    const handleChange = event => {
        const result = event.target.value.replace(/[^a-z]/gi, '');
        setMessage(result);
    };

    const addBtnFn = (row) => { 
        setBtnmodal(true); 
        setMessage(row.button_name);
        setbtnid(row.id);
    }

    const addnewbtnFn = async (e) => { 
        let BUTTON_NAME = btnmessage; 
        const res = await addBtn({ id, BUTTON_NAME });
        if (res) {
            if (res.flag) {
                setmodal(false);
                refreshFn();
            }
            else {
                alert(res.message);
            }
        }
        fatchbtnList();
        setBtnmodal(false);
    }

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-lg-1 col-6 col-md-6 col-xs-12'>
                        <h5 className='text-start'>Page List</h5>
                    </div>
                    <div className='col-lg-2 col-6 col-md-6 col-xs-12 text-end justify-content-center'>
                        <Save label="Refresh" primaryBtn onClick={refreshFn} />
                        <Save label="Add" primaryBtn onClick={addFn} />
                        <Save label="Save" primaryBtn onClick={saveFn} />
                        {/* <Save label="Save Button" primaryBtn /> */}
                        <Save label="Reset" primaryBtn />
                    </div>
                </div>
                {modal && (
                    <div className='row align-items-center justify-content-between mb-3'>
                        <div className='col-12 d-flex'>
                            <div>Page Name : <input type="text" value={name} onChange={e => setName(e.target.value)} /></div>
                            <div className='ms-3'>Page Sub Name : <input type="text" value={namesub1} onChange={e => setNamesub1(e.target.value)} /></div>
                            <div className='ms-3'>Page Sub Name : <input type="text" value={namesub2} onChange={e => setNamesub2(e.target.value)} /></div>
                            <div className='ms-5 mt-1'><input type="checkbox" checked={isActive} className="checkBox me-1" />Active</div>
                            <Save label="Update" className='ms-5' primaryBtn onClick={updateFn} />
                        </div>
                    </div>
                )}
                <div className='row align-items-start justify-content-start'>
                    <div className='col-xl-4 col-lg-6 col-12 col-md-9 col-sm-12 border'>
                        <DataTable columns={colums} data={pagelist} />
                    </div>
                    {modal && (
                        <div className='col-xl-3 col-lg-6 col-12 col-md-9 col-sm-12 border'>
                            <div className='d-flex align-items-center justify-content-end'>
                                <p className='m-2 me-2'>Add Button</p>
                                <img src={Add} width={20} style={{ cursor: "pointer" }} onClick={addBtnFn} />
                            </div>
                            <DataTable columns={btncolums} data={btnlist} />
                        </div>
                    )}
                    {Btnmodal && (
                        <div className='d-flex align-items-start justify-content-center'>
                            <div>
                                <input type='text' placeholder='Button Name' value={btnmessage} onChange={handleChange} />
                            </div>
                            <Save label="Add Button" className='ms-5' primaryBtn onClick={addnewbtnFn} />
                        </div>
                    )}
                </div>
            </div>
        </>
    )
} 