import React, { useState } from 'react';
import { Button, DatePicker, Form, Input, Modal } from 'antd';
import { CriarCliente, CriarClienteRequest } from '../../../services/clientes/api';
import { toast } from 'react-toastify';
import moment from 'moment';
import InputCpfMask from '../../InputCpfMask';

type CriarClienteModalProps = {
    atualizar: any
}

const CriarClienteModal: React.FC<CriarClienteModalProps> = ({ atualizar }) => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();

    const [cliente, setCliente] = useState<CriarClienteRequest>({
        nome: "",
        cpf: "",
        dataNascimento: new Date(),
    });

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        CriarCliente(cliente).then((res) => {
            if (res.status === 200) {
                setCliente({ cpf: '', nome: '', dataNascimento: new Date() })
                form.resetFields();
                toast.success("Cliente criado com sucesso!")
                setIsModalVisible(false);
                atualizar();
            }
        })
    };

    const handleCancel = () => {
        setCliente({ cpf: '', nome: '', dataNascimento: new Date() })
        form.resetFields();
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal destroyOnClose title="Cadastro de Cliente" visible={isModalVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="criar-cliente-form" key="submit" htmlType="submit">
                    Cadastrar
                </Button>
            ]}>
                <Form
                    id="criar-cliente-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="CPF" name="cpf" rules={[{ required: true, message: 'Informe o CPF valido' }]}>
                        <InputCpfMask value={cliente?.cpf} onChange={(e) => setCliente({ ...cliente, cpf: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, message: 'Informe o Nome' }]}>
                        <Input placeholder="Nome Do cliente" value={cliente?.cpf} onChange={(e) => setCliente({ ...cliente, nome: e.target.value })} />
                    </Form.Item>
                    <Form.Item label="Data De Nascimento" name="dataNascimento" rules={[{ required: true, message: 'Informe a Data de Nascimento' }]}>
                        <DatePicker
                            value={moment(cliente.dataNascimento)}
                            format={'DD/MM/YYYY'}
                            onChange={(e) => setCliente({ ...cliente, dataNascimento: e?.toDate() || new Date() })}
                        />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default CriarClienteModal;