import React, { useState } from 'react';
import { Button, Checkbox, Form, Input, Modal, Select } from 'antd';
const { Option } = Select;

const FilmeModal: React.FC = () => {
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
            <Modal title="Cadastro de Filme" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button key="submit" type="primary" onClick={handleOk}>
                    Cadastrar
                </Button>
            ]}>
                <Form form={form} layout="vertical">
                    <Form.Item required label="Título">
                        <Input placeholder="Título" />
                    </Form.Item>
                    <Form.Item required label="Classificação indicativa">
                        <Select style={{ width: 120 }}>
                            <Option value="10">10 anos</Option>
                            <Option value="12">12 anos</Option>
                            <Option value="14">14 anos</Option>
                            <Option value="16">16 anos</Option>
                            <Option value="18">18 anos</Option>
                        </Select>
                    </Form.Item>
                    <Form.Item>
                        <Checkbox>Lançamento</Checkbox>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default FilmeModal;