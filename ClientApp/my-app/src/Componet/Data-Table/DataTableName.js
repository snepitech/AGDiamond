import { React } from 'react';
import DataTable, { createTheme } from "react-data-table-component";
import DataTableExtensions from "react-data-table-component-extensions";
import './datTableaname.css';

createTheme('solarized', {
    text: {
        primary: '#000000',
        secondary: '#000000',
    },
    background: {
        default: '#fff',
    },
    context: {
        background: '#fff',
        text: '#000000',
    },
    divider: {
        default: '#ccc',
    },
    action: {
        button: 'rgba(0,0,0,.54)',
        hover: 'rgba(0,0,0,.08)',
        disabled: 'rgba(0,0,0,.12)',
    },
    striped: {
        default: '#fff',
    },
}, 'dark');

const paginationComponentOptions = {
    selectAllRowsItem: true,
    selectAllRowsItemText: "ALL"
};

export default function EnhanceTable({ columns, data, ...props }) {

    const tableData = {
        columns,
        data
    };
    if (props.pagination === undefined) {
        props.pagination = true;
    }
    //   const rowPreExpanded = row => row.defaultExpanded
    return (
        <div>
            <DataTable
                title={props.title}
                columns={columns}
                data={data}
                noHeader
                defaultSortField={props.sortField}
                defaultSortAsc={false}
                pagination={props.pagination}
                // selectableRows
                noTableHead={props.noTableHead}
                noDataComponent=""
                dense
                fixedHeader
                fixedHeaderScrollHeight={props.height}
                highlightOnHover
                // paginationRowsPerPageOptions={[25, 50, 100, 200]}
                paginationComponentOptions={paginationComponentOptions}
                theme="solarized"
                // paginationPerPage={25}
                rowsPerPageOptions={[]}
            />
        </div>
    )
}