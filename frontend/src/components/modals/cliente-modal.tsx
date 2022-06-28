import React, { useState } from 'react';
import { Button, DatePicker, Form, Input, Modal } from 'antd';

const ClienteModal: React.FC = () => {
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        setIsModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    return (
        <>
            <Button type="primary" onClick={showModal}>
                Cadastrar
            </Button>
            <Modal title="Cadastro de Cliente" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button key="submit" type="primary" onClick={handleOk}>
                    Cadastrar
                </Button>
            ]}>
                <Form form={form} layout="vertical">
                    <Form.Item required label="Nome">
                        <Input placeholder="Nome Do cliente" />
                    </Form.Item>
                    <Form.Item required label="CPF">
                        <Input placeholder="CPF Do Cliente" />
                    </Form.Item>
                    <Form.Item required label="Data De Nascimento">
                        <DatePicker />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default ClienteModal;