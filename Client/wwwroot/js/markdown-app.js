class MarkdownApp {
    diagramEngine = null;

    constructor() {

    }

    createDiagramEngine = (diagram) => {
        this.diagramEngine = new DiagramEngine();
        this.diagramEngine.init(diagram);
    }
}

class DiagramEngine {
    /**
     * The selector to target the parent element of the diagram canvas.
     */
    containerSelector = '#diagram-container';

    /**
     * The parent element of the diagram canvas.
     */
    container = null;

    /**
     * The canvas element to draw the diagram components to.
     */
    canvas = null;

    /**
     * The 2D context of the diagram canvas.
     */
    context = null;

    /**
     * The diagram entity that contains the components to render.
     */
    diagram = null;

    connectors = [];

    options = {
        canvasFill: '#f9f9f9'
    };

    cardinalDirection = {
        left: 0,
        top: 1,
        right: 2,
        bottom: 3
    };

    constructor() {

    }

    init = (diagram) => {
        this.diagram = diagram || { components: [] };

        // Query the container of the digram canvas for use in automatic resizing of the canvas and other actions.
        this.container = document.querySelector(this.containerSelector);

        if (!this.container) {
            throw "Diagram canvas container does not exist.";
        }

        this.canvas = this.container.querySelector('#diagram-canvas');

        if (!this.canvas) {
            throw "Diagram engine canvas does not exist.";
        }

        // Get the 2d context for the diagram canvas.
        this.context = this.canvas.getContext('2d');

        this.registerCanvasEvents();

        // Ensure that the canvas matches the dimensions on the parent element with the canvas is loaded.
        this.resizeCanvas();

        // Register the event to automatically resize the canvas when the window resizes.
        window.addEventListener('resize', this.resizeCanvas);

        // Start the draw loop.
        window.requestAnimationFrame(this.draw);
    }

    /**
     * Register any required events on the canvas element.
     */
    registerCanvasEvents = () => {
        this.canvas.addEventListener('mousemove', (event) => {
            const rect = this.canvas.getBoundingClientRect();

            const x = event.clientX - rect.left;
            const y = event.clientY - rect.top;

            document.querySelector('.diagram-coordinates').textContent = `x: ${x} , y: ${y}`;
        });
    }

    /**
     * Draws the diagram components to the canvas context.
     * @param {any} timestamp The time since the last draw loop iteration.
     */
    draw = (timestamp) => {
        // Ensure we clear the context it draw iteration.
        this.clear();

        // Implement the engine defaults into the context.
        this.context.fillStyle = this.options.canvasFill;
        this.context.fillRect(0, 0, this.context.canvas.width, this.context.canvas.height);

        if (this.diagram.components) {
            // Configure render fragments.
            this.diagram.components.forEach((component) => {
                // Configure the connections as fragments.
                this.mapConnections(component);

            });

            // Render components to context.
            this.diagram.components.forEach((component) => {
                // Draw the component.
                this.context.fillStyle = component.backgroundColor;
                this.context.fillRect(component.position.x, component.position.y, component.position.width, component.position.height);

                this.context.beginPath();
                this.context.strokeStyle = component.strokeColor;
                this.context.rect(component.position.x, component.position.y, component.position.width, component.position.height);
                this.context.stroke();

                // Draw connectors.
                this.drawConnections();
            });
        }

        // Continue to the next draw loop iteration.
        window.requestAnimationFrame(this.draw);
    }

    drawConnections = () => {
        this.connectors.forEach((connector) => {

            this.context.beginPath();

            this.context.lineCap = 'round';
            this.context.strokeStyle = connector.strokeColor;
            this.context.lineWidth = connector.thickness;
            this.context.moveTo(connector.start.x, connector.start.y);
            this.context.lineTo(connector.end.x, connector.end.y);

            this.context.stroke();
        });
    }

    computeConnectorCardinalPosition = (component, cardinalDirection) => {
        let position = {};

        switch (cardinalDirection) {
            case this.cardinalDirection.left:
                position.x = component.position.x;
                position.y = component.position.y + (component.position.height / 2);
                break;
            case this.cardinalDirection.top:
                position.x = component.position.x + (component.position.width / 2);
                position.y = component.position.y;
                break;
            case this.cardinalDirection.right:
                position.x = component.position.x + component.position.width;
                position.y = component.position.y + (component.position.height / 2);
                break;
            case this.cardinalDirection.bottom:
                position.x = component.position.x + (component.position.width / 2);
                position.y = component.position.y + component.position.height;
                break;
        }

        return position;
    }

    mapConnections = (sourceComponent) => {
        if (sourceComponent.connections && sourceComponent.connections.length > 0) {
            sourceComponent.connections.forEach((connection) => {
                let targetComponent = this.diagram.components.find(x => x.id === connection.componentId);

                if (targetComponent) {
                    // computer x1, y1.
                    let sourcePosition = this.computeConnectorCardinalPosition(sourceComponent, connection.sourceCardinal);
                    let targetPosition = this.computeConnectorCardinalPosition(targetComponent, connection.targetCardinal);

                    this.connectors.push({
                        start: {
                            x: sourcePosition.x,
                            y: sourcePosition.y
                        },
                        end: {
                            x: targetPosition.x,
                            y: targetPosition.y
                        }
                    })
                }
            });
        }
    }

    /**
     * Clears the current canvas context and any fragments/connectors.
     */
    clear = () => {
        this.connectors = [];
        this.context.clearRect(0, 0, this.context.canvas.width, this.context.canvas.height);
    }

    /**
     * Resize the context canvas to match the size of the parent element.
     */
    resizeCanvas = () => {
        this.context.canvas.width = this.container.clientWidth;
        this.context.canvas.height = this.container.clientHeight;
    }

    addComponent = (component) => {
        this.diagram.components.push(component);
    }

    setComponentProperty = (componentId, property, value) => {
        let existingComponent = this.diagram.components.find(x => x.id === componentId);

        if (existingComponent) {
            var propertySegments = property.split('.');

            if (propertySegments.length > 0) {
                var ref;

                propertySegments.forEach((propertySegment, index) => {
                    if ((propertySegments.length - 1) !== index) {
                        ref = existingComponent[propertySegment];
                    }
                });

                ref[propertySegments[propertySegments.length - 1]] = value;
            }
            else {
                existingComponent[propertySegments[0]] = value;
            }
        }
    }
}

/**
 * When the DOM is loaded add the markdown app to the window.
 */
document.addEventListener('DOMContentLoaded', () => {
    window.markdownApp = new MarkdownApp();
})