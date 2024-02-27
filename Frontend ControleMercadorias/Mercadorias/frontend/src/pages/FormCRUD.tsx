// Dentro de FormCRUD.tsx

import React, { useState, useEffect } from 'react';
import Form from '../components/Formulario/form';
import { fetchForms, deleteForms, updateForms, createForms } from '../services/apiService';
import './FormCRUD.css';

interface FormsItem {
    id: number;
    name: string;
    registrationNumber: string;
    manufacturer: string;
    type: string;
    description: string;
    horaConsulta: string;
    category: string;
}

interface FormData {
    id?: number;  // Inclua 'id' se necessário
    name: string;
    registrationNumber: string;
    manufacturer: string;
    type: string;
    description: string;
    horaConsulta: string;
    category: string;
}

const FormCRUD = () => {
    const [formsList, setFormsList] = useState<FormsItem[]>([]);
    const [editingForms, setEditingForms] = useState<FormData | null>(null);
    const [selectedType , setSelectedType] = useState('');


    useEffect(() => {
        const loadForms = async () => {
            const data = await fetchForms();
            setFormsList(data);
        }; 
        loadForms();
    }, []);

        const handleEdit = async (formsData: FormData) => {
                setEditingForms(formsData);
        }

        const handleDelete = async (formsId: number) => {
                await deleteForms(formsId);
                const data = await fetchForms();
                setFormsList(data);
        }

        const handleSubmit = async (data: FormData) => {
                if (editingForms) {
                        await updateForms(editingForms.id, data);
                } else {
                        await createForms(data);
                }
                const forms = await fetchForms();
                setFormsList(forms);
                setEditingForms(null);
        }

        const handleTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
                setSelectedType(e.target.value);
        }

        const filteredForms = selectedType ? formsList.filter((form) => form.type === selectedType) : formsList;

        //Selecione o type de cada formulário para criar a lista de opções do select
        const types = formsList.map((form) => form.type);
        const uniqueTypes = Array.from(new Set(types));

        return (
                <div className="form-crud">
                        <h1>Formulário</h1>
                        <select className='select' onChange={handleTypeChange} value={selectedType} name='type'>
                                <option value="">Todos</option>
                                {uniqueTypes.map((type, index) => (
                                        <option key={index} value={type}>{type}</option>
                                ))}
                        </select>
                        <table>
                                <thead>
                                        <tr>
                                                <th>Nome</th>
                                                <th>Registro</th>
                                                <th>Fabricante</th>
                                                <th>Tipo</th>
                                                <th>Descrição</th>
                                                <th>Hora da Consulta</th>
                                                <th>Ações</th>
                                        </tr>
                                </thead>
                                <tbody>
                                        {filteredForms.map((form, index) => (
                                                <tr key={index}>
                                                        <td>{form.name}</td>
                                                        <td>{form.registrationNumber}</td>
                                                        <td>{form.manufacturer}</td>
                                                        <td>{form.type}</td>
                                                        <td>{form.description}</td>
                                                        <td>{form.horaConsulta}</td>
                                                        <td>
                                                                <button onClick={() => handleEdit(form)}>Editar</button>
                                                                <button onClick={() => handleDelete(form.id)}>Deletar</button>
                                                        </td>
                                                </tr>
                                        ))}
                                </tbody>
                        </table>
                        <h2>Editar Formulário</h2>
                        <Form initialData={editingForms} onSubmit={handleSubmit} uniqueTypes={uniqueTypes} />
                </div>
                
        );
}

export default FormCRUD;