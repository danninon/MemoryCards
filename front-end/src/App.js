import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css';
import Accordion from 'react-bootstrap/Accordion';
import CreateGroup from './components/CreateGroup';
import SelectGroup from './components/SelectGroup';
import StudyCarousel from './components/StudyCarousel';
import axios from 'axios';
import {useState, useEffect} from 'react';

const App = () => { 
const BASE_URL = 'https://localhost:7186/StudyGroups' //change to make dynamic //StudyGroups is derived from controller name minus the word controller
const [groupNames, setGroupNames] = useState([]); //variable that is set in the, function
const [group, setGroup] = useState([]);
 
useEffect(() => {getGroupNames()}, []); //function, array of dependencies, function is called every time the value changes

const createGroup = async(cards) => {
  try{
    // console.log("cards: " + cards);
    await axios.post(BASE_URL, cards);
    getGroupNames();
  }catch(e){
    console.log('Error: ', e);
  }
}

const getGroupNames = async() => {
  try{
    // console.log("cards: " + cards);
    const {data} = await axios.get(`${BASE_URL}/group-names`, groupNames);
    setGroupNames(data);
  }catch(e){
    console.log('Error: ', e);
  }
}

const getGroup = async(groupName) => {
  try{
    // console.log("cards: " + cards);
    const {data} = await axios.get(`${BASE_URL}/${groupName}`);
    setGroup(data);
  }catch(e){
    console.log('Error: ', e);
  }
}

const deleteGroup = async(groupName) => {
  try{
    // console.log("cards: " + cards);
    const {data} = await axios.delete(`${BASE_URL}/${groupName}`);
    getGroupNames(data);
  }catch(e){
    console.log('Error: ', e);
  }
}


return <div className = 'App'>
  <h2>Study Groups</h2>
  <Accordion >
      <Accordion.Item eventKey="0">
        <Accordion.Header>Upload New</Accordion.Header>
        
        <Accordion.Body> 
        <CreateGroup createGroup={createGroup}/>
        </Accordion.Body>
       
      </Accordion.Item>
      <Accordion.Item eventKey="1">
        <Accordion.Header>Your Groups</Accordion.Header>
        <Accordion.Body>
           <SelectGroup 
            groupNames = {groupNames}
            getGroup={getGroup}
            deleteGroup={deleteGroup}
           />
        </Accordion.Body>
      </Accordion.Item>
    </Accordion>
    <StudyCarousel group={group} />
</div>

}
export default App;
