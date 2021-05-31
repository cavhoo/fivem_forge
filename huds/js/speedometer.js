class Speedometer {
    constructor(stage, renderer) {
        this.stage = stage;
        this.renderer = renderer;
        this.maxRpm = 9000;
        this.zeroRpmAngle = -60;
        this.maxRpmAngle = 179;
        this.redZoneAngle = 150;
        this.rpmAngleRange = Math.abs(this.zeroRpmAngle) + this.maxRpmAngle;
        this.container = null;
        this.gauge = null;
        this.gaugeMask = null;
        this.needle = null;
        this.redZone = null;
        this.init();
    }

    init() {
        const clientWidth = getClientWidth();
        const clientHeight = getClientHeight();


        this.container = new PIXI.Container();
        this.gauge = new PIXI.Sprite.from("assets/rpmgauge.png");
        this.gauge.scale.x = 0.1;
        this.gauge.scale.y = 0.1;
        this.gauge.anchor.set(0.5, 0.54);

        this.gaugeMask = new PIXI.Sprite.from("assets/rpmgauge_mask.png");
        this.gaugeMask.scale.x = 0.1;
        this.gaugeMask.scale.y = 0.1;
        this.gaugeMask.tint = 0xFF0000;
        this.gaugeMask.anchor.set(0.5, 0.54);

        // TODO: Create mask sprite that can be rotated to mark specifc region as redline.
        this.redZone = new PIXI.Graphics();
        this.redZone.beginFill(0xFF0000, 1);
        this.redZone.drawRect(0, 0, 200, 200);
        this.redZone.endFill();
        this.redZone.position.x = -100;
        this.redZone.position.y = -100;

        const bounds = new PIXI.Rectangle(0, 0, 200, 200);
        const redZoneTexture = this.renderer.generateTexture(this.redZone, PIXI.SCALE_MODES.NEAREST, 1, bounds)
        const redZoneSprite = new PIXI.Sprite(redZoneTexture);


        this.needle = new PIXI.Sprite.from("assets/rpmneedle.png");
        this.needle.scale.x = 0.12;
        this.needle.scale.y = 0.12;
        this.needle.pivot.x = 1191;
        this.needle.pivot.y = 29.16;
        this.needle.angle = 179;
        this.needle.x = this.gauge.x;
        this.needle.y = this.gauge.y;


        this.container.addChild(this.gauge, this.gaugeMask, this.needle, redZoneSprite);
        this.gaugeMask.mask = redZoneSprite;
        this.container.x = clientWidth - this.container.width - 200;
        this.container.y = clientHeight - this.container.height - 150;
        this.stage.addChild(this.container);
    }

    visible(visible) {
        this.container.visible = visible;
    }

    getContainer() {
        return this.container;
    }

    setRpm(rpm) {
        const ratio = rpm / this.maxRpm;
        const angle = this.rpmAngleRange * ratio;
        this.needle.angle = this.zeroRpmAngle + angle;
    }
}
