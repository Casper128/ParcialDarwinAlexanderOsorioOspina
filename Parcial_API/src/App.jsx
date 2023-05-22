import { useEffect, useState } from 'react';
import { useFetch } from './useFetch';
import { useForm } from './useForm';
import './App.css';

const initialForm={
  id:'',
  entranceGate:'',
}

export const App = () => {
  const [result, setResult] = useState(false);
  const [message, setMessage] = useState(false);
  const [message2, setmessage2] = useState(false);
  const [message3, setmessage3] = useState(false);
  const { handleGet, handleSubmit} = useFetch();
  const { id, entranceGate, onInputChange, onResetForm} = useForm(initialForm);
  const [data, setData] = useState([])
  const getUrl = "https://localhost:7120/api/Tickets/Get";
  const putUrl = `https://localhost:7120/api/Tickets/Put/${id}`;
  const deleteUrl = `https://localhost:7120/api/Tickets/Delete/${id}`;

  useEffect(() => {
    handleGet(getUrl, setData);
  }, []);

const handleEditSubmit=async (event,id,entranceGate,putUrl,onResetForm,getUrl, setData)=>{
  const response=await handleSubmit(event,id,entranceGate,putUrl,onResetForm,getUrl, setData);
  if(response===true){
    setResult(true);
    setMessage(false);
    setmessage2(false);
    setmessage3(false);
  }
  if(response.status===400 || response.error==="Boleta no válida"){
    setMessage(true);
    setResult(false);
    setmessage2(false);
    setmessage3(false);
  }
  if(response.error==="Boleta ya usada"){
    setMessage(false);
    setResult(false);
    setmessage2(true);
    setmessage3(false);
  }
  if(response.error==="EntranceGate esta vacio"){
    setMessage(false);
    setResult(false);
    setmessage2(false);
    setmessage3(true);
  }
}

  return (
    <>
      <div className="container">
        <h1>Tabla de datos</h1>
        {/* <!-- Formulario --> */}
        <div className="row">
          <div className="col-md-6 offset-md-3">
            <h2>Agregar nuevo registro</h2>
            {result && <h3>Boleta válida, puede ingresar al concierto</h3>}
            {message && <h3>Boleta no válida</h3>}
            {message2 && <h3>Boleta ya usada</h3>}
            {message3 && <h3>EntranceGate esta vacio</h3>}       
            <form id="formulario" onSubmit={(event)=>handleEditSubmit(event,id,entranceGate,putUrl,onResetForm,getUrl, setData)}>
              <div className="form-group">
                <label htmlFor="id">ID:</label>
                <input type="text" name="id" className="form-control" id="id" required value={id} onChange={onInputChange}/>
              </div>
              <div className="form-group">
                <label htmlFor="entranceGate">Entrance Gate:</label>
                <select type="text" name="entranceGate" className="form-control" id="entranceGate"  value={entranceGate}  onChange={onInputChange} >
                  <option value="">Default</option>
                  <option value="Norte">Norte</option>
                  <option value="Sur">Sur</option>
                  <option value="Oriental">Oriental</option>
                  <option value="Occidental">Occidental</option>
                </select>
              </div>
              <button type="submit" className="btn btn-primary">Guardar</button>
            </form>
          </div>
        </div>
      </div>

      {/* <!-- Tabla de datos --> */}
      <div className="container table-container">
        <table className="table table-striped">
          <thead>
            <tr>
              <th>#</th>
              <th>ID</th>
              <th>Is Used</th>
              <th>Entrance Gate</th>
              <th>Use Date</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item,index) => (
              <tr key={item.id}>
                <td>{index}</td>
                <td>{item.id}</td>
                <td>{item.isUsed ? 'Sí' : 'No'}</td>
                <td>{item.entranceGate == null ? "null" : item.entranceGate}</td>
                <td>{item.useDate == null ? "null" : item.useDate}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  )
}

