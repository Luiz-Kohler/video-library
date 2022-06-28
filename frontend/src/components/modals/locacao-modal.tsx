import React, { useState } from 'react';
import { Button, Checkbox, Form, Input, Modal, Select } from 'antd';
const { Option } = Select;

const LocacaoModal: React.FC = () => {
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
            <Modal title="Cadastro de Locação" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} footer={[
                <Button key="back" onClick={handleCancel}>
                    Voltar
                </Button>,
                <Button key="submit" type="primary" onClick={handleOk}>
                    Cadastrar
                </Button>
            ]}>
                <Form form={form} layout="vertical">
                    <Form.Item required label="Cliente">
                        <Select
                            showSearch
                            style={{ width: 200 }}
                            placeholder="Buscar Cliente"
                            optionFilterProp="children"
                            filterOption={(input, option) => (option!.children as unknown as string).includes(input)}
                            filterSort={(optionA, optionB) =>
                                (optionA!.children as unknown as string)
                                    .toLowerCase()
                                    .localeCompare((optionB!.children as unknown as string).toLowerCase())
                            }
                        >
                            <Option value="1">1 - Luiz</Option>
                            <Option value="2">2 - Ama</Option>
                            <Option value="3">3 - Tiane</Option>
                            <Option value="4">4 - Muito</Option>
                        </Select>
                    </Form.Item>
                    <Form.Item required label="Filme">
                        <Select
                            showSearch
                            style={{ width: 200 }}
                            placeholder="Buscar Filme"
                            optionFilterProp="children"
                            filterOption={(input, option) => (option!.children as unknown as string).includes(input)}
                            filterSort={(optionA, optionB) =>
                                (optionA!.children as unknown as string)
                                    .toLowerCase()
                                    .localeCompare((optionB!.children as unknown as string).toLowerCase())
                            }
                        >
                            <Option value="1">1 - Luiz</Option>
                            <Option value="2">2 - Ama</Option>
                            <Option value="3">3 - Tiane</Option>
                            <Option value="4">4 - Muito</Option>
                        </Select>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
};

export default LocacaoModal;