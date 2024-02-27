import React, { useState, useEffect } from 'react';
import './Form.css'; 
import generatePdf from '../../utils/generatePdf';

interface FormData {
  id?: number;
  name: string;
  registrationNumber: string;
  manufacturer: string;
  type: string;
  description: string;
  horaConsulta: string;
  category: string;
}

interface FormProps {
  initialData?: FormData | null | undefined;
  onSubmit: (data: FormData) => void;
  uniqueTypes: string[];
}

const Form: React.FC<FormProps> = ({ initialData, onSubmit, uniqueTypes }) => {
  const [formData, setFormData] = useState({
    name: '', registrationNumber: '', manufacturer: '', type: '', description: '', horaConsulta: '', category: '',
  });
  //Selecione o type de cada formulário para criar a lista de opções do select
  const [formsList, setFormsList] = useState<FormData[]>([]);
  const [selectedType, setSelectedType] = useState('');


  useEffect(() => {
    const loadForms = async () => {
      const response = await fetch('http://localhost:5001/product');
      const data = await response.json();
      setFormsList(data);
    };
    loadForms();
  }, []);

    useEffect(() => {
      if (initialData) {
        setFormData(initialData);
      }
    }, [initialData]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    onSubmit(formData);
  };

  const handleClear = () => {
    setFormData({ name: '', registrationNumber: '', manufacturer: '', type: '', description: '', horaConsulta: '', category: '' });
  }

  const handleGeneratePdf = () => {
    generatePdf([formData]);
  };

  const handleTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedType(e.target.value);  
    }



  //Selecione o type de cada formulário para criar a lista de opções do select  
 
  
  // Componente form com dropdown adicionado
  return (
    <form onSubmit={handleSubmit} className="form-container">
      <div className="form-group">
        <label htmlFor="name">Nome:</label>
        <input type="text" name="name" value={formData.name} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="registrationNumber">CNPJ:</label>
        <input type="text" name="registrationNumber" value={formData.registrationNumber} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="manufacturer">Fabricante:</label>
        <input type="text" name="manufacturer" value={formData.manufacturer} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="type">Tipo:</label>
        <input type="text" name="type" value={formData.type} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="description">Descrição:</label>
        <textarea name="description" value={formData.description} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="horaConsulta">Hora da Consulta:</label>
        <input type="time" name="horaConsulta" value={formData.horaConsulta} onChange={handleChange} />
      </div>
      <div className="form-group">
        <label htmlFor="type">Type:</label>
        <select onChange={handleChange} value={formData.type} name="type">
          <option value="">Selecione</option>
          {uniqueTypes.map((type) => (
            <option key={type} value={type}>
              {type}
            </option>
          ))}
        </select>
      </div>    
      <div className="form-group">
        <button type="submit">Enviar</button>
        <button type="button" onClick={handleClear}>Limpar</button>
        <button type="button" onClick={handleGeneratePdf}>Gerar PDF</button>
      </div>
    </form>
  );
}

export default Form;