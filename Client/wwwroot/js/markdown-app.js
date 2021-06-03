var markdownApp = {
    context: null,
    diagram: null,
    container: null,

    configureDiagram: (diagram) => {
        markdownApp.diagram = diagram || { components: [] };

        markdownApp.container = document.querySelector('#diagram-container');

        if (markdownApp.container) {
            let diagramCanvas = markdownApp.container.querySelector('#diagram-canvas');

            if (diagramCanvas) {
                markdownApp.context = diagramCanvas.getContext('2d');

                markdownApp.context.canvas.width = markdownApp.container.clientWidth;
                markdownApp.context.canvas.height = markdownApp.container.clientHeight;

                diagramCanvas.addEventListener('mousedown', (event) => {
                    const rect = diagramCanvas.getBoundingClientRect()

                    const x = event.clientX - rect.left;
                    const y = event.clientY - rect.top;

                    console.log(`x: ${x} // y: ${y}`);
                });
            }
        }

        window.requestAnimationFrame(markdownApp.draw);

        window.addEventListener('resize', () => {
            markdownApp.context.canvas.width = markdownApp.container.clientWidth;
            markdownApp.context.canvas.height = markdownApp.container.clientHeight;
        });
    },

    draw: (timestamp) => {
        // Set context defaults.
        markdownApp.context.fillStyle = '#dddddd';
        markdownApp.context.fillRect(0, 0, markdownApp.container.clientWidth, markdownApp.container.clientHeight);

        if (markdownApp.diagram.components) {
            // Render components to context.
            markdownApp.diagram.components.forEach((component) => {
                markdownApp.context.fillStyle = component.material.backgroundColor;
                markdownApp.context.fillRect(component.position.x, component.position.y, component.position.width, component.position.height);
            });
        }

        window.requestAnimationFrame(markdownApp.draw);
    }
}

class MarkdownApp {

}

class DiagramEngine {

}

window.markdownApp = markdownApp;