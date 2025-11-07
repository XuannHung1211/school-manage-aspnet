import { create } from "zustand";

interface ModalState {
  modalKey: string | null; // tên hoặc id của modal đang mở
  openModal: (key: string) => void;
  closeModal: () => void;
}

export const useModalStore = create<ModalState>((set) => ({
  modalKey: null,
  openModal: (key) => set({ modalKey: key }),
  closeModal: () => set({ modalKey: null }),
}));
