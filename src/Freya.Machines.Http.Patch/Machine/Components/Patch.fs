﻿namespace Freya.Machines.Http.Patch.Machine.Components

#nowarn "46"

open Freya.Machines.Http.Machine.Components
open Freya.Machines.Http.Machine.Specifications
open Freya.Machines.Http.Patch.Machine.Specifications
open Freya.Types.Http.Patch
open Hephaestus

(* Patch *)

[<RequireQualifiedAccess>]
module Patch =

    (* Name *)

    [<Literal>]
    let Name =
        "http-patch"

    (* Component *)

    let rec private main s =
        Main.specification Name (
            s, Method.specification Name (set [ PATCH ]) (
                s, Existence.specification Name (
                    Responses.Moved.specification Name (
                        continuation),
                    Preconditions.Common.specification Name (
                        Preconditions.Unsafe.specification Name (
                            Conflict.specification Name (
                                continuation))))))

    and private continuation =
        Operation.specification Name PATCH (
            Responses.Created.specification Name (
                Responses.Other.specification Name (
                    Responses.Common.specification Name)))

    let component =
        { Metadata =
            { Name = Name
              Description = None }
          Requirements =
            { Required = set [ Core.Name ]
              Preconditions = List.empty }
          Operations =
            [ Splice (Key [ Core.Name; "fallback"; "fallback-decision" ], Right, main) ] }