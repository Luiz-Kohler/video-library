import React, { useEffect, useState } from 'react';
import { Button, DatePicker, Form, Input, Modal } from 'antd';
import { AtualizarCliente, AtualizarClienteRequest, BuscarCliente } from '../../../services/clientes/api';
import { toast } from 'react-toastify';
import moment from 'moment';

type AtualizarClienteModalProps = {
    isVisible: boolean;
    setVisableFalse: any;
    id: number;
    atualizar: any
}
const AtualizarClienteModal: React.FC<AtualizarClienteModalProps> = ({ isVisible, setVisableFalse, atualizar, id }) => {
    const [form] = Form.useForm();

    const [cliente, setCliente] = useState<AtualizarClienteRequest>({
        id: id,
        dataNascimento: new Date(),
        nome: ""
    });

    useEffect(() => {
        if (id !== 0) {
            BuscarCliente(id).then(res => {
                setCliente({ id: id, dataNascimento: res.dataNascimento, nome: res.nome });
                form.setFieldsValue({ id: id, dataNascimento: moment(res.dataNascimento), nome: res.nome });
            })
        }
    }, [id])

    const handleOk = () => {
        AtualizarCliente(cliente).then((res) => {
            if (res.status === 200) {
                setCliente({ id: 0, nome: '', dataNascimento: new Date() })
                setVisableFalse(false);
                atualizar();
                toast.success("Cliente atualizado com sucesso!")
            }
        })
    };

    const handleCancel = () => {
        setVisableFalse();
    };

    return (
        <>
            <Modal title="Atualização de Cliente" visible={isVisible} onOk={form.submit} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button type='primary' form="atualizar-cliente-form" key="submit" htmlType="submit">
                    Atualizar
                </Button>
            ]}>
                <Form
                    id="atualizar-cliente-form"
                    form={form}
                    layout="vertical"
                    labelCol={{ span: 8 }}
                    wrapperCol={{ span: 16 }}
                    onFinish={handleOk}
                    autoComplete="off"
                >
                    <Form.Item label="Data De Nascimento" name="dataNascimento" rules={[{ required: true, message: 'Informe a Data de Nascimento' }]}>
                        <DatePicker
                            value={moment(cliente.dataNascimento)}
                            format={'DD/MM/YYYY'}
                            onChange={(e) => setCliente({ ...cliente, dataNascimento: e?.toDate() || new Date() })}
                        />
                    </Form.Item>
                    <Form.Item label="Nome" name="nome" rules={[{ required: true, message: 'Informe o Nome' }]}>
                        <Input placeholder="Nome Do cliente" value={cliente?.nome} onChange={(e) => setCliente({ ...cliente, nome: e.target.value })} />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default AtualizarClienteModal;