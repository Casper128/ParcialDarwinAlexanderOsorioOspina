import React, { useEffect } from 'react'

export const useFetch = () => {

    
    const handleGet = async (url,setData) => {
        try {
            const requestOptions = {
                method: 'GET',
                redirect: 'follow'
            };
            const response = await fetch(url, requestOptions);
            const result = await response.json();
            console.log(response);
            setData(result);
        } catch (error) {
            console.log('error', error);
        }
        
    };




const handleSubmit = async (event,id,gate,url,onResetForm,getUrl, setData) => {
    event.preventDefault();
    try {
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: id,
                entrancegate: gate
            })
        };
        console.log(requestOptions.body)
        const response = await fetch(url, requestOptions);
        console.log(response)
        // Restablece el formulario
        onResetForm();
        handleGet(getUrl, setData);
    } catch (error) {
        console.log('error', error);
    }
};


const handleDelete = async (id) => {
    // Abre el modal y establece el item seleccionado
    try {
        const requestOptions = {
            method: 'DELETE',
            redirect: 'follow'
        };
        await fetch(`https://localhost:7208/api/Roles/Delete/${id}`, requestOptions);
        // Actualizar la lista de roles después del borrado
        const updatedData = dataRols.filter((value) => value.id !== id);
        setDataRols(updatedData);
    } catch (error) {
        console.log('error', error);
    }
};

return (
    {
        handleGet,
        handleSubmit,
        handleDelete
    }
)
}
