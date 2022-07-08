import React from 'react';
import '../commen.css';
import { Button } from 'evergreen-ui';
export default function BtnSave(props) {
    return (
        <Button onClick={props.onClick} id="saveBtn" appearance="primary" intent="success" className={`${props.className} ${props.linkBtn ? 'linkBtn' : false} ${props.primaryBtn ? 'primaryBtn' : false}`}>{props.label}</Button>
    )
}