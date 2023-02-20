function CarregarDadosGastosTotais() {
    $.ajax({
        url: '/Despesas/GastosTotais',
        method: 'POST',
        success: function (dados) {
            new Chart(document.getElementById("GraficoGastosTotais"), {
                type: 'line',

                data: {
                    labels: PegarMeses(dados),

                    datasets: [{
                        label: "Total gasto",
                        data: PegarValores(dados),
                        backgroundColor: "#ecf0f1",
                        borderColor: "#2980b9",
                        fill: false,
                        spanGaps: false
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: "Total gasto"
                    }
                }
            });
        }
    });
}