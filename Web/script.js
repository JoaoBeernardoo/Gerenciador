document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("modal-tarefa");
    const form = document.getElementById("form-tarefa");
    const btnIncluir = document.getElementById("btn-incluir");
    const closeModal = document.getElementById("close-modal");
    const tarefasContainer = document.getElementById("tarefas-container");

    let tarefas = [];

    function abrirModal(tarefa = null) {
        modal.style.display = "block";
        document.getElementById("modal-title").innerText = tarefa ? "Editar Tarefa" : "Nova Tarefa";
        if (tarefa) {
            document.getElementById("nome").value = tarefa.nome;
            document.getElementById("custo").value = tarefa.custo;
            document.getElementById("data").value = tarefa.data;
            document.getElementById("tarefa-id").value = tarefa.id;
        } else {
            form.reset();
            document.getElementById("tarefa-id").value = "";
        }
    }

    function fecharModal() {
        modal.style.display = "none";
    }

    function renderizarTarefas() {
        tarefasContainer.innerHTML = "";
        tarefas.sort((a, b) => a.ordem - b.ordem);
        tarefas.forEach(tarefa => {
            const div = document.createElement("div");
            div.className = "tarefa" + (tarefa.custo >= 1000 ? " alta" : "");
            div.innerHTML = `
                <div class="tarefa-info">
                    <strong>${tarefa.nome}</strong>
                    <span>Custo: R$ ${tarefa.custo.toFixed(2)}</span>
                    <span>Data limite: ${tarefa.data}</span>
                </div>
                <div class="tarefa-botoes">
                    <button class="btn btn-small btn-warning" onclick="editarTarefa(${tarefa.id})">✏️</button>
                    <button class="btn btn-small btn-danger" onclick="confirmarExclusao(${tarefa.id})">🗑️</button>
                    <button class="btn btn-small" onclick="mover(${tarefa.id}, -1)">🔼</button>
                    <button class="btn btn-small" onclick="mover(${tarefa.id}, 1)">🔽</button>
                </div>
            `;
            tarefasContainer.appendChild(div);
        });
    }

    function editarTarefa(id) {
        const tarefa = tarefas.find(t => t.id === id);
        abrirModal(tarefa);
    }

    function confirmarExclusao(id) {
        if (confirm("Tem certeza que deseja excluir esta tarefa?")) {
            tarefas = tarefas.filter(t => t.id !== id);
            renderizarTarefas();
        }
    }

    function mover(id, direcao) {
        const index = tarefas.findIndex(t => t.id === id);
        const novoIndex = index + direcao;
        if (novoIndex >= 0 && novoIndex < tarefas.length) {
            [tarefas[index].ordem, tarefas[novoIndex].ordem] = [tarefas[novoIndex].ordem, tarefas[index].ordem];
            renderizarTarefas();
        }
    }

    form.addEventListener("submit", function (e) {
        e.preventDefault();
        const id = document.getElementById("tarefa-id").value;
        const nome = document.getElementById("nome").value.trim();
        const custo = parseFloat(document.getElementById("custo").value);
        const data = document.getElementById("data").value;

        if (tarefas.some(t => t.nome === nome && t.id != id)) {
            alert("Já existe uma tarefa com esse nome.");
            return;
        }

        if (id) {
            const tarefa = tarefas.find(t => t.id == id);
            tarefa.nome = nome;
            tarefa.custo = custo;
            tarefa.data = data;
        } else {
            const novoId = Date.now();
            tarefas.push({
                id: novoId,
                nome,
                custo,
                data,
                ordem: tarefas.length + 1
            });
        }

        fecharModal();
        renderizarTarefas();
    });

    btnIncluir.addEventListener("click", () => abrirModal());
    closeModal.addEventListener("click", fecharModal);
    window.onclick = function (event) {
        if (event.target == modal) {
            fecharModal();
        }
    };

    
    tarefas = [
        { id: 1, nome: "Comprar materiais", custo: 950, data: "2025-08-01", ordem: 1 },
        { id: 2, nome: "Pagar servidor", custo: 1200, data: "2025-08-05", ordem: 2 },
    ];
    renderizarTarefas();
});
