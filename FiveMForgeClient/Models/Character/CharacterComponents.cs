extern alias CFX;

using System;
using System.Collections.Generic;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Controller.Language;

namespace FiveMForgeClient.Models.Character
{
    public static class CharacterComponents
    {
        public static readonly CharacterComponent[] Components =
        {
            new(LanguageController.Translate("sex"), "sex", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("mom"), "mom", 21, 21, 0.6f, 0.65f),
            new(LanguageController.Translate("dad"), "dad", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("resemblance"), "face_md_weight", 50, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("skin_tone"), "skin_md_weight", 50, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_1"), "nose_1", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_2"), "nose_2", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_3"), "nose_3", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_4"), "nose_4", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_5"), "nose_5", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("nose_6"), "nose_6", 0, -10, 0.6f, 0.65f),
            new(LanguageController.Translate("cheeks_1"), "cheeks_1", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("cheeks_2"), "cheeks_2", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("cheeks_3"), "cheeks_3", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("lip_fullness"), "lip_thickness", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("jaw_bone_width"), "jaw_1", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("jaw_bone_length"), "jaw_2", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("chin_height"), "chin_1", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("chin_length"), "chin_2", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("chin_width"), "chin_3", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("chin_hole"), "chin_4", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("neck_thickness"), "neck_thickness", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("hair_1"), "hair_1", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("hair_2"), "hair_2", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("hair_color_1"), "hair_color_1", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("hair_color_2"), "hair_color_2", 0, 0, 0.6f, 0.65f),
            new(LanguageController.Translate("tshirt_1"), "tshirt_1", 0, 0, 0.75f, 0.15f, 8),
            new(LanguageController.Translate("tshirt_2"), "tshirt_2", 0, 0, 0.75f, 0.15f, "tshirt_1"),
            new(LanguageController.Translate("torso_1"), "torso_1", 0, 0, 0.75f, 0.15f, 11),
            new(LanguageController.Translate("torso_2"), "torso_2", 0, 0, 0.75f, 0.15f, "torso_1"),
            new(LanguageController.Translate("decals_1"), "decals_1", 0, 0, 0.75f, 0.15f, 10),
            new(LanguageController.Translate("decals_2"), "decals_2", 0, 0, 0.75f, 0.15f, "decals_1"),
            new(LanguageController.Translate("arms"), "arms", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("arms_2"), "arms_2", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("pants_1"), "pants_1", 0, 0, 0.8f, -0.5f, 4),
            new(LanguageController.Translate("pants_2"), "pants_2", 0, 0, 0.8f, -0.5f, "pants_1"),
            new(LanguageController.Translate("shoes_1"), "shoes_1", 0, 0, 0.8f, -0.8f, 6),
            new(LanguageController.Translate("shoes_2"), "shoes_2", 0, 0, 0.8f, -0.8f, "shoes_1"),
            new(LanguageController.Translate("mask_1"), "mask_1", 0, 0, 0.6f, 0.65f, 1),
            new(LanguageController.Translate("mask_2"), "mask_2", 0, 0, 0.6f, 0.65f, "mask_1"),
            new(LanguageController.Translate("bproof_1"), "bproof_1", 0, 0, 0.75f, 0.15f, 9),
            new(LanguageController.Translate("bproof_2"), "bproof_2", 0, 0, 0.75f, 0.15f, "bproof_1"),
            new(LanguageController.Translate("chain_1"), "chain_1", 0, 0, 0.6f, 0.65f, 7),
            new(LanguageController.Translate("chain_2"), "chain_2", 0, 0, 0.6f, 0.65f, "chain_1"),
            new(LanguageController.Translate("helmet_1"), "helmet_1", -1, -1, 0.6f, 0.65f, 0),
            new(LanguageController.Translate("helmet_2"), "helmet_2", 0, 0, 0.6f, 0.65f, "helmet_1"),
            new(LanguageController.Translate("glasses_1"), "glasses_1", 0, 0, 0.6f, 0.65f, 1),
            new(LanguageController.Translate("glasses_2"), "glasses_2", 0, 0, 0.6f, 0.65f, "glasses_1"),
            new(LanguageController.Translate("watches_1"), "watches_1", -1, -1, 0.75f, 0.15f, 6),
            new(LanguageController.Translate("watches_2"), "watches_2", 0, 0, 0.75f, 0.15f, "watches_1"),
            new(LanguageController.Translate("bracelets_1"), "bracelets_1", -1, -1, 0.75f, 0.15f, 7),
            new(LanguageController.Translate("bracelets_2"), "bracelets_2", 0, 0, 0.75f, 0.15f, "bracelets_1"),
            new(LanguageController.Translate("bag"), "bags_1", 0, 0, 0.75f, 0.15f, 5),
            new(LanguageController.Translate("bag_color"), "bags_2", 0, 0, 0.75f, 0.15f, "bags_1"),
            new(LanguageController.Translate("eye_color"), "eye_color", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("eye_squint"), "eye_squint", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_size"), "eyebrows_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_type"), "eyebrows_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_color_1"), "eyebrows_3", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_color_2"), "eyebrows_4", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_height"), "eyebrows_5", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("eyebrow_depth"), "eyebrows_6", 0, -10, 0.4f, 0.65f),
            new(LanguageController.Translate("makeup_type"), "makeup_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("makeup_thickness"), "makeup_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("makeup_color_1"), "makeup_3", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("makeup_color_2"), "makeup_4", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("lipstick_type"), "lipstick_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("lipstick_thickness"), "lipstick_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("lipstick_color_1"), "lipstick_3", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("lipstick_color_2"), "lipstick_4", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("ear_accessories"), "ears_1", -1, -1, 0.4f, 0.65f, 2),
            new(LanguageController.Translate("ear_accessories_color"), "ears_2", 0, 0, 0.4f, 0.65f, "ears_1"),
            new(LanguageController.Translate("chest_hair"), "chest_1", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("chest_hair_1"), "chest_2", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("chest_color"), "chest_3", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("bodyb"), "bodyb_1", -1, -1, 0.75f, 0.15f),
            new(LanguageController.Translate("bodyb_size"), "bodyb_2", 0, 0, 0.75f, 0.15f),
            new(LanguageController.Translate("bodyb_extra"), "bodyb_3", -1, -1, 0.4f, 0.15f),
            new(LanguageController.Translate("bodyb_extra_thickness"), "bodyb_4", 0, 0, 0.4f, 0.15f),
            new(LanguageController.Translate("wrinkles"), "age_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("wrinkle_thickness"), "age_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("blemishes"), "blemishes_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("blemishes_size"), "blemishes_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("blush"), "blush_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("blush_1"), "blush_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("blush_color"), "blush_3", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("complexion"), "complexion_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("complexion_1"), "complexion_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("sun"), "sun_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("sun_1"), "sun_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("freckles"), "moles_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("freckles_1"), "moles_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("beard_type"), "beard_1", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("beard_size"), "beard_2", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("beard_color_1"), "beard_3", 0, 0, 0.4f, 0.65f),
            new(LanguageController.Translate("beard_color_2"), "beard_4", 0, 0, 0.4f, 0.65f)
        };
    }
}