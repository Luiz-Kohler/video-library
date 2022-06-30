import React, { useState } from 'react';
import { Button, Checkbox, Form, Input, Modal, Select } from 'antd';
import { CriarFilme, CriarFilmeRequest } from '../../../services/filmes/api';
import { Classificacao } from '../../../enums/filme';
import { toast } from 'react-toastify';
const { Option } = Select;

type CriarFilmeModalProps = {
    atualizar: any
}

const CriarFilmeModal: React.FC<CriarFilmeModalProps> = ({ atualizar }) => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();

    const [filme, setFilme] = useState<CriarFilmeRequest>({
        id: 0,
        classificacao: Classificacao.Livre,
        titulo: "",
        ehLancamento: false
    });

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        CriarFilme(filme).then((res) => {
            console.log(filme)
            if (res.status === 200) {
                setFilme({ id: 0, titulo: "", classificacao: Classificacao.Livre, ehLancamento: false })
                form.resetFields();
                toast.success("Filme criado com sucesso!")
                setIsModalVisible(false);
                atualizar();
            }
        })
    };

    const handleCancel = () => {
        setFilme({ id: 0, titulo: "", classificacao: Classificacao.Livre, ehLancamento: false })
        form.resetFields();
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal destroyOnClose title="Cadastro de Filme" visible={isModalVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="criar-filme-form" key="submit" htmlType="submit">
                    Cadastrar
                </Button>
            ]}>
                <Form
                    id="criar-filme-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >   
                    <Form.Item label="Id"  name="id" rules={[{ required: true, message: 'Informe o Id' }]}>
                        <Input type={"number"} placeholder="Id" value={filme?.id} onChange={(e) => setFilme({ ...filme, id: parseInt(e.target.value) })}/>
                    </Form.Item>
                    <Form.Item label="Titulo" name="titulo" rules={[{ required: true, message: 'Informe o Titulo' }]}>
                        <Input placeholder="Titulo" value={filme?.titulo} onChange={(e) => setFilme({ ...filme, titulo: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="Classificação" name="classificacao" rules={[{ required: true, message: 'Informe classificação' }]}>
                        <Select style={{ width: 120 }} onChange={(value) => setFilme({ ...filme, classificacao: parseInt(value) })}>
                            <Option value="0">Livre</Option>
                            <Option value="10">Dez anos</Option>
                            <Option value="12">Doze anos</Option>
                            <Option value="14">Catorze anos</Option>
                            <Option value="16">Dezesseis anos</Option>
                            <Option value="18">Dezoito anos</Option>
                        </Select>
                    </Form.Item>
                    <Form.Item label="Lançamento">
                        <Checkbox value={filme?.ehLancamento} onChange={(e) => setFilme({ ...filme, ehLancamento: e.target.checked })} />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default CriarFilmeModal;