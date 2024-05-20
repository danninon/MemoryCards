import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';

const SelectGroup = ({ groupNames, getGroup, deleteGroup }) => {
   return <Table hover>
        <tbody>
            {groupNames.map((groupName, index) =>
                <tr key={index}>

                    <td> {groupName} </td>

                    <td> <Button onClick={() =>
                        getGroup(groupName)}>Start</Button> </td>
                    <td><Button onClick={() =>
                        deleteGroup(groupName)} variant='danger'>Delete</Button></td>

                </tr>
            )}
        </tbody>
    </Table>
}

export default SelectGroup;