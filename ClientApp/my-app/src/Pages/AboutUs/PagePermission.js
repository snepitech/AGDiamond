import './pagepermission.css';
import '../../Componet/commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Axios from 'axios';
import { Container } from 'react-bootstrap';
import Checkbox from 'rc-checkbox';
// import 'rc-checkbox/assets/index.css';
import { Button, SavedIcon } from 'evergreen-ui';
import Save from '../../Componet/Button/BtnSave';
import DataTable from '../../Componet/Data-Table/DataTable';
import DataTableName from '../../Componet/Data-Table/DataTableName';
import React, { useState, useEffect } from "react";

import { API_URLS } from "../../Componet/API/allAPI";
import { Input } from 'postcss';
const { RoleMast, User, PageList } = API_URLS;

export default function PagePermission() {

    const [isCheckAll, setIsCheckAll] = useState(false);
    const [user, setuser] = useState([]);
    const [pagelist, setPageList] = useState([]);
    const [usermasterList, setuserMasterList] = useState([]);

    const fetchCartList = async () => {
        let obj = {};
        const { data } = await Axios.post(
            RoleMast.Select, obj
        );
        if (data.data != null) {
            const list = data.data.list;
            setuserMasterList(list);
        }
    }

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
        fetchCartList();
    }, []);

    const getUserList = async (e) => {
        const { data } = await Axios.post(
            User.Select, { role_id: e }
        );
        if (data.data != null) {
            const list = data.data.list;
            setuser(list);
        }
        else {
            setuser([]);
        }
        fetchPageList();
    }

    const ResetFn = () => {
        fetchCartList(false);
        getUserList();
        fetchPageList();
        setIsCheckAll(false);
    }

    const columsName = [
        {
            name: '',
            cell: (d) => <> <input type="checkbox" value={user.code} className="checkBox" /></>,
            width: '20px',
        },
        {
            name: 'User',
            selector: 'code',
            sortable: true,
            width: "120px",
        },
    ]

    const colums = [
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
            name: 'View',
            cell: (d) => <><input type="checkbox" className="checkBox" /></>,
            width: '50px',
        },
        {
            name: 'Insert',
            cell: (d) => <><input type="checkbox" className="checkBox" /></>,
            width: '50px',
        },
        {
            name: 'Update',
            width: '55px',
            cell: (d) => <><input type="checkbox" className="checkBox" /></>,
        },
        {
            name: 'Delete',
            width: '50px',
            cell: (d) => <><input type="checkbox" className="checkBox" /></>,
        },
    ]

    const numbers = ['Save', 'Role', 'User', 'Page', 'Reset'];

    const ButtonFn = (e) => {
        if (e === 'Page') {
            window.open('./PageList');
        }
    }

    return (
        <>
            <div className='mt-5 pt-3 container-fluid'>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-lg-2 col-6 col-md-6 col-xs-12'>
                        <h5 className='text-start'>Page Permission</h5>
                    </div>
                    <div className='col-lg-3 col-6 col-md-6 col-xs-12'>

                        {/* <Button marginRight={5} appearance="primary" intent="success" className='pagebtn'>Save</Button>
                        <Button marginRight={5} appearance="primary" intent="success" className='pagebtn' >Role</Button>
                        <Button marginRight={5} appearance="primary" intent="success" className='pagebtn' >User</Button>
                        <Button marginRight={5} appearance="primary" intent="success" className='pagebtn' >Page</Button>
                        <Button appearance="primary" intent="danger" className='pagebtn' onClick={ResetFn} >Reset</Button> */}
                        {/* <Save label="priyank" primaryBtn marginRight={5} /> */}

                        {numbers.map((numbers => <Save label={numbers} value={numbers} primaryBtn marginRight={2} onClick={(e) => ButtonFn(numbers)} />))}
                    </div>
                </div>
                <div className='row align-items-center justify-content-between'>
                    <div className='col-lg-2 col-12 col-md-6'>
                        <select className='form-control' onChange={e => getUserList(e.target.value)}>
                            <option value="0">Select Role</option>
                            {usermasterList.map((user, i) =>
                                <option key={user.id} value={user.id} >{user.name}</option>
                            )}
                        </select>
                    </div>
                </div>
            </div>
            <div className='mt-1 container-fluid'>
                <div className='row align-items-start justify-content-start'>
                    <div className='nametable col-xl-2 col-12 col-sm-12 border'>
                        <DataTableName
                            columns={columsName}
                            data={user}
                        />
                    </div>
                    <div className='col-xl-4 col-lg-6 col-12 col-md-9 col-sm-12 border'>
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