import React, {useState} from 'react';
import {Link} from "react-router-dom";
import {ITableProps, kaReducer, Table} from 'ka-table';
import {DataType, EditingMode, SortingMode} from 'ka-table/enums';
import {Play} from "react-feather";

var sessions = [
    {
        id: "asdfasdf",
        user: {
            browser: "chrome",
            country: "russia"
        },
        duration: "3:23",
        activityLevel: "average",
        dateTime: "12:00",
        siteId: "sstu-ru",
        pages: [
            {},
            {},
            {}
        ]
    }
]

function PlayButton(props) {
    console.log(props)
    return (
        <Link to={`/site/${props.rowData.siteId}/session/${props.rowData.id}/recording`}
              className={"flex justify-center p-3 rounded-full hover:bg-gray-300 text-center w-12"}>
            <Play className="inline-block"/>
        </Link>
    )
}

const tablePropsInit: ITableProps = {
    columns: [
        {key: 'play recording', cell: (props) => (<PlayButton {...props}/>), style: {width: 60}},
        {key: 'activityLevel', title: 'Activity level', dataType: DataType.String},
        {key: 'user', title: 'User info', dataType: DataType.Object},
        {key: 'dateTime', title: 'Date and time', dataType: DataType.String},
        {key: 'duration', title: 'Session duration', dataType: DataType.String},
        {key: 'pages.length', title: 'Visited pages', dataType: DataType.Number},
    ],
    data: sessions,
    editingMode: EditingMode.Cell,
    rowKeyField: 'id',
    sortingMode: SortingMode.Single,
};

function SessionList(props) {
    // let params = useParams();

    const [tableProps, changeTableProps] = useState(tablePropsInit);
    const dispatch: DispatchFunc = (action) => {
        changeTableProps((prevState: ITableProps) => kaReducer(prevState, action));
    };

    return (
        <Table
            {...tableProps}
            dispatch={dispatch}
        />
    );
}

export default SessionList;